using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using System.Data;

namespace Zillow
{
    class Properties
    {
        DAL dal = new DAL();
        public void ZillowApiResponse()
        {
            string SelectIdQuery = "SELECT Id, PropertyId FROM PropertyId";
            dal.ExecuteSelectionQuery(SelectIdQuery);
            if (dal.dt != null)
            {
                foreach (DataRow dr in dal.dt.Rows)
                {
                    var Id = dr[0];
                    var PropertyId = dr[1];
                    var client = new RestClient("https://zillow-data.p.rapidapi.com/property/detail?zpid=" + PropertyId + "");
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("X-RapidAPI-Key", "6616dadbe0mshc0711e8fb4834c6p1a853ajsnddc6238469a2");
                    request.AddHeader("X-RapidAPI-Host", "zillow-data.p.rapidapi.com");
                    IRestResponse response = client.Execute(request);
                    dynamic content = JsonConvert.DeserializeObject(response.Content);

                    var Url = "https://www.zillow.com" + content.hdpUrl;
                    var Address = content.address.streetAddress + ", " + content.address.city + ", " + content.address.state + " " + content.address.zipcode;
                    var ProviderName = content.attributionInfo.agentName;
                    var ProviderPhone = content.attributionInfo.agentPhoneNumber;
                    var BuiltIn = content.yearBuilt;

                    var PriceSqft = content.priceHistory[0].pricePerSquareFoot;
                    if(PriceSqft != null)
                    {
                        PriceSqft = "$" + PriceSqft;
                    }
                    var Price = content.price;
                    if(Price != null)
                    {
                        Price = "$" + Price.ToString("N0");
                    }

                    var Bed = content.bedrooms;
                    var Bath = content.bathrooms;
                    var SqFt = content.livingArea.ToString("N0");
                    var Type = content.homeType;
                    var Zestimate = content.zestimate;
                    if(Zestimate != null)
                    {
                        Zestimate = "$" + Zestimate.ToString("N0");
                    }
                    var Latitude = content.latitude;
                    var Longitude = content.longitude;
                    var PropertyType = content.propertyTypeDimension;
                    var LotSize = content.resoFacts.lotSize.ToString("N0");
                    var LivableArea = content.livingArea;
                    if(LivableArea!= null)
                    {
                        LivableArea = LivableArea.ToString("N0") + " sqft";
                    }
                    var ParkingSpaces = content.resoFacts.garageParkingCapacity;
                    var DaysOnZillow = content.timeOnZillow;
                    var ListUpdated = content.attributionInfo.lastUpdated;
                    if(ListUpdated != null)
                    {
                        var ListUpdated1 = ListUpdated.ToString().Split(" ");
                        var date = Convert.ToDateTime(ListUpdated1[0]).ToString("MMMM dd, yyyy");
                        var time  = Convert.ToDateTime(ListUpdated1[1]).ToString("hh:mm tt");
                        ListUpdated = date + " at " + time;
                    }
                    var RentZestimate = content.rentZestimate;
                    if(RentZestimate != null)
                    {
                        RentZestimate = "$" + RentZestimate.ToString("N0");
                    }

                    var PropertyInsertQuery = "INSERT INTO Properties(Url, Address, ProviderName, ProviderPhone, BuiltIn, PriceSqft, Price, Bed, Bath, SqFt, Type, Zestimate, Latitude, Longitude, PropertyType, LotSize, LivableArea, ParkingSpaces, DaysOnZillow, ListUpdated, RentZestimate) VALUES('" + Url + "', '" + Address + "', '" + ProviderName + "', '" + ProviderPhone + "', '" + BuiltIn + "', '" + PriceSqft + "', '" + Price + "', '" + Bed + "', '" + Bath + "', '" + SqFt + "', '" + Type + "', '" + Zestimate + "', '" + Latitude + "', '" + Longitude + "', '" + PropertyType + "', '" + LotSize + "', '" + LivableArea + "', '" + ParkingSpaces + "', '" + DaysOnZillow + "', '" + ListUpdated + "', '" + RentZestimate + "')";
                    dal.ExecuteQuery(PropertyInsertQuery);

                    var GetIdQuery = "Select Id from Properties where Url = '" + Url + "'";
                    dal.ExecuteScalar(GetIdQuery);

                    var PriceHistory = content.priceHistory;
                    if (PriceHistory != null)
                    {
                        foreach (var objPrice in PriceHistory)
                        {
                            DateTime DateConverter = objPrice.date; 
                            var Date = DateConverter.ToString("MM/d/yyyy");
                            var Event = objPrice["event"];
                            var price = objPrice.price;
                            var priceChangeRate = objPrice.priceChangeRate * 100;
                            if (price != null)
                            {
                                if (objPrice.pricePerSquareFoot != null &&  objPrice.priceChangeRate != null)
                                {
                                    if (priceChangeRate != 0)
                                    {
                                        price = "$" + price.ToString("N0") + " (" + priceChangeRate + "%) " + " $" + objPrice.pricePerSquareFoot + "/sqft";
                                    }
                                    else
                                    {
                                        price = "$" + price.ToString("N0") + " $" + objPrice.pricePerSquareFoot + "/sqft";
                                    }
                                }
                                else if(objPrice.pricePerSquareFoot == null)
                                {
                                    price = "$" + price.ToString("N0") + " (" + priceChangeRate + "%) ";
                                }
                                else
                                {
                                    price = "$" + price.ToString("N0") + " $" + objPrice.pricePerSquareFoot + "/sqft";
                                }
                            }

                            var PriceHistoryQuery = "INSERT INTO PriceHistory (PropertyId,Date,Event,Price) VALUES('" + dal.Id + "', '" + Date + "', '" + Event + "', '" + price + "')";
                            dal.ExecuteQuery(PriceHistoryQuery);
                        }
                    }
                    

                    var TaxHistory = content.taxHistory;
                    if (TaxHistory != null)
                    {
                        foreach (var objTax in TaxHistory)
                        {
                            long time = objTax.time;
                            DateTimeOffset dateTimeOffSet = DateTimeOffset.FromUnixTimeMilliseconds(time);
                            var Year = dateTimeOffSet.Year;
                            string PropertyTax = "";
                            if (objTax.taxPaid != null)
                            {
                                decimal Tax = objTax.taxPaid;
                                decimal TaxVal= Math.Round(Tax);
                                if (TaxVal != null)
                                {
                                    double? TaxIncreaseRate = objTax.taxIncreaseRate * 100;
                                    double? PropertyTaxPercent = Math.Round(TaxIncreaseRate.GetValueOrDefault(), 1);
                                    if (PropertyTaxPercent != 0)
                                    {
                                        PropertyTax = "$" + TaxVal.ToString("N0") + " (" + PropertyTaxPercent + "%)";
                                    }
                                    else if(TaxVal.ToString() != "")
                                    {
                                        PropertyTax = "$" + TaxVal.ToString("N0");
                                    }
                                }
                            }

                            double? TaxValueIncreaseRate = objTax.valueIncreaseRate * 100;
                            double? TaxPercent = Math.Round(TaxValueIncreaseRate.GetValueOrDefault(), 1);
                            var ObjTaxValue = objTax.value.ToString("N0");
                            string TaxAssessment = "";
                            if (TaxPercent != 0)
                            {
                                TaxAssessment = "$" + ObjTaxValue + " (" + TaxPercent + "%)";
                            }
                            else if(ObjTaxValue != "")
                            {
                                TaxAssessment = "$" +  ObjTaxValue;
                            }

                            var TaxHistoryQuery = "INSERT INTO TaxHistory(PropertyId,Year,PropertyTax,TaxAssessment) VALUES('" + dal.Id + "', '" + Year + "', '" + PropertyTax + "', '" + TaxAssessment + "')";
                            dal.ExecuteQuery(TaxHistoryQuery);
                        }
                    }
                }
            }
        }
    }
}
