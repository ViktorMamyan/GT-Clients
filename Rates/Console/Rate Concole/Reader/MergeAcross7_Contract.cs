using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ConsoleApp1
{
    public class MergeAcross7_Contract
    {
        public List<ReadyData_7> Read7_Contract(List<List<CellInfo>> ListData)
        {
            List<hotel_7> hotelList = new List<hotel_7>();

            hotel_7 hotel = new hotel_7();

            CancelPolicy policyItem = new CancelPolicy();

            obligatoryService ExtraSservice = new obligatoryService();

            string CurrentHeader = string.Empty;

            int priceID = 0;

            int pID = 0;
            int rID = 0;

            int ServiceID = 0;

            string LastCaption = string.Empty;

            foreach (var rowData in ListData)
            {
                for (int DD = 0; DD < rowData.Count; DD++)
                {
                    if (rowData[DD].StyleID.Replace("\"", "").ToLower() == "s63".ToLower())
                    {
                        CurrentHeader = string.Empty;

                        priceID = 0;
                        pID = 0;
                        rID = 0;

                        LastCaption = string.Empty;

                        if (hotel == null || hotel.HotelName == null)
                        {
                            hotel = new hotel_7();
                            hotel.HotelName = rowData[DD].Value.Trim();

                            policyItem = new CancelPolicy();

                            continue;
                        }

                        hotelList.Add(hotel);
                        hotel = new hotel_7();
                        hotel.HotelName = rowData[DD].Value.Trim();

                        policyItem = new CancelPolicy();

                        continue;
                    }
                    else
                    {
                        switch (rowData[DD].Value.Trim())
                        {
                            case "Region":
                                LastCaption = "Region"; continue;
                            case "Board":
                                LastCaption = "Board"; continue;
                            case "Category":
                                LastCaption = "Category"; continue;
                            case "Currency":
                                LastCaption = "Currency"; continue;
                            case "Accommodation nights":
                                LastCaption = "Accommodation nights"; continue;
                            case "Periods":
                                LastCaption = "Periods"; continue;
                            case "Reservation dates":
                                LastCaption = "Reservation dates"; continue;
                            case "Cancellation / No show policy":
                                LastCaption = "Cancellation";
                                break;
                            default:
                                break;
                        }

                        if (LastCaption == "Region") { hotel.Region = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Board") { hotel.Board = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Category") { hotel.Category = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Currency") { hotel.Currency = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Accommodation nights")
                        {
                            if (hotel.NightsFrom == 0)
                            {
                                hotel.NightsFrom = Convert.ToInt32(rowData[DD].Value.Trim());
                            }
                            else
                            {
                                hotel.NightsTill = Convert.ToInt32(rowData[DD].Value.Trim());
                            }

                            continue;
                        }
                        if (LastCaption == "Periods")
                        {
                            string period = rowData[DD].Value.Trim();
                            string[] parts = period.Split(new[] { "\n" }, StringSplitOptions.None);

                            if (parts.Length == 2 && DateTime.TryParseExact(parts[0].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date1) && DateTime.TryParseExact(parts[1].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date2))
                            {
                                periods Local_period = new periods();

                                Local_period.Start = date1;
                                Local_period.End = date2;

                                hotel.Periods.Add(Local_period);
                            }
                            //LastCaption = string.Empty;
                            pID++;
                            continue;
                        }
                        if (LastCaption == "Reservation dates")
                        {
                            string resdate = rowData[DD].Value.Trim();
                            string[] parts = resdate.Split(new[] { "\n" }, StringSplitOptions.None);

                            if (parts.Length == 2 && DateTime.TryParseExact(parts[0].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime rdate1) && DateTime.TryParseExact(parts[1].Trim(), "dd.MM.yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime rdate2))
                            {
                                reservationdates reservationDates = new reservationdates();

                                reservationDates.Start = rdate1;
                                reservationDates.End = rdate2;

                                hotel.ReservationDates.Add(reservationDates);
                            }

                            rID++;
                            if (rID == pID)
                            {
                                LastCaption = string.Empty;
                            }
                            continue;
                        }
                    }

                    if (rowData[DD].StyleID.Replace("\"", "").ToLower() == "s76".ToLower())
                    {
                        if (rowData[DD].MergeAcross == 7)
                        {
                            CurrentHeader = "Room";

                            rooms Rooms = new rooms();
                            Rooms.Room = rowData[DD].Value.Trim();

                            hotel.Rooms.Add(Rooms);

                            continue;
                        }
                    }
                    else if (hotel.Rooms != null && hotel.Rooms.Count > 0 && rowData[DD].StyleID.Contains("s73"))
                    {
                        if (rowData[DD].Value.Trim() != "Release periods")
                        {
                            int roomQTY = hotel.Rooms.Count - 1;

                            accommodation accommodation1 = new accommodation();
                            accommodation1.Accommodation = rowData[DD].Value.Trim();

                            hotel.Rooms[roomQTY].Accommodation.Add(accommodation1);

                            priceID = 0;

                            continue;
                        }
                    }
                    else if (rowData[DD].StyleID.Contains("s77"))
                    {
                        priceID++;

                        int roomQTY = hotel.Rooms.Count - 1;
                        int AccQTY = hotel.Rooms[roomQTY].Accommodation.Count - 1;

                        price price = new price();

                        price.PeriodsStart = hotel.Periods[priceID - 1].Start;
                        price.PeriodsEnd = hotel.Periods[priceID - 1].End;

                        price.ReservationStart = hotel.ReservationDates[priceID - 1].Start;
                        price.ReservationEnd = hotel.ReservationDates[priceID - 1].End;

                        if (decimal.TryParse(rowData[DD].Value.Trim(), out decimal result))
                        {
                            price.Price = Convert.ToDecimal(result);
                        }
                        else
                        {
                            price.Price = (decimal?)(null);
                        }

                        hotel.Rooms[roomQTY].Accommodation[AccQTY].Price.Add(price);

                        continue;
                    }

                    if (rowData[DD].Value.Trim() == "Limit")
                    {
                        LastCaption = "Limit";

                        continue;
                    }
                    else if (rowData[DD].Value.Trim() == "penalty")
                    {
                        LastCaption = "penalty";

                        continue;
                    }
                    else if (rowData[DD].Value.Trim() == "No Show")
                    {
                        LastCaption = "No Show";

                        continue;
                    }

                    if (rowData[DD].StyleID.Replace("\"", "").ToLower() == "s96".ToLower())
                    {
                        LastCaption = "ObligatoryService";

                        ServiceID = 1;

                        ExtraSservice.ServiceName = rowData[DD].Value.Trim();

                        continue;
                    }
                    if (LastCaption == "ObligatoryService")
                    {
                        switch (ServiceID)
                        {
                            case 1:
                                string period = rowData[DD].Value.Trim();
                                string[] parts = period.Split(new[] { "\n" }, StringSplitOptions.None);

                                if (parts.Length == 2 && DateTime.TryParseExact(parts[0].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date1) && DateTime.TryParseExact(parts[1].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date2))
                                {
                                    ExtraSservice.PeriodStart = date1;
                                    ExtraSservice.PeriodEnd = date2;
                                }

                                ServiceID++;
                                continue;
                            case 2:
                                ExtraSservice.Meal = rowData[DD].Value.Trim();
                                ServiceID++;
                                continue;
                            case 3:
                                ExtraSservice.AdultPrice = Convert.ToDecimal(rowData[DD].Value.Trim());
                                ServiceID++;
                                continue;
                            case 4:
                                if (rowData[DD].Value.Trim() != null && !string.IsNullOrEmpty(rowData[DD].Value.Trim()))
                                {
                                    ExtraSservice.ChildOldPrice = Convert.ToDecimal(rowData[DD].Value.Trim());
                                }
                                ServiceID++;
                                continue;
                                case 5:
                                if (rowData[DD].Value.Trim() != null && !string.IsNullOrEmpty(rowData[DD].Value.Trim()))
                                {
                                    string age = rowData[DD].Value.Trim();
                                    string[] ageparts = age.Split(new[] { "-" }, StringSplitOptions.None);

                                    if (ageparts.Length == 2)
                                    {
                                        ExtraSservice.ChildOldFrom = Convert.ToInt32(ageparts[0]);
                                        ExtraSservice.ChildOldTo = Convert.ToInt32(ageparts[1]);
                                    }
                                }
                                ServiceID++;
                                continue;
                            case 6:
                                if (rowData[DD].Value.Trim() != null && !string.IsNullOrEmpty(rowData[DD].Value.Trim()))
                                {
                                    ExtraSservice.ChildMiddlePrice = Convert.ToDecimal(rowData[DD].Value.Trim());
                                }
                                ServiceID++;
                                continue;
                                case 7:
                                if (rowData[DD].Value.Trim() != null && !string.IsNullOrEmpty(rowData[DD].Value.Trim()))
                                {
                                    string age = rowData[DD].Value.Trim();
                                    string[] ageparts = age.Split(new[] { "-" }, StringSplitOptions.None);

                                    if (ageparts.Length == 2)
                                    {
                                        ExtraSservice.ChildMiddleFrom = Convert.ToInt32(ageparts[0]);
                                        ExtraSservice.ChildMiddleTo = Convert.ToInt32(ageparts[1]);
                                    }
                                }
                                ServiceID++;
                                continue;
                                case 8:
                                if (rowData[DD].Value.Trim() != null && !string.IsNullOrEmpty(rowData[DD].Value.Trim()))
                                {
                                    ExtraSservice.ChildYoungPrice = Convert.ToDecimal(rowData[DD].Value.Trim());
                                }
                                ServiceID++;
                                continue;
                                case 9:
                                if (rowData[DD].Value.Trim() != null && !string.IsNullOrEmpty(rowData[DD].Value.Trim()))
                                {
                                    string age = rowData[DD].Value.Trim();
                                    string[] ageparts = age.Split(new[] { "-" }, StringSplitOptions.None);

                                    if (ageparts.Length == 2)
                                    {
                                        ExtraSservice.ChildYoungFrom = Convert.ToInt32(ageparts[0]);
                                        ExtraSservice.ChildYoungTo = Convert.ToInt32(ageparts[1]);
                                    }
                                }
                                else
                                {
                                    if (ExtraSservice.ChildMiddleFrom.HasValue)
                                    {
                                        ExtraSservice.ChildYoungFrom = 0;
                                        ExtraSservice.ChildYoungTo = ExtraSservice.ChildMiddleFrom.Value;
                                    }
                                }
                                ServiceID++;
                                continue;
                                case 10:
                                ExtraSservice.Inf_w_o_seat = Convert.ToDecimal(rowData[DD].Value.Trim());
                                ServiceID++;
                                continue;
                                case 11:
                                ExtraSservice.PerService = Convert.ToDecimal(rowData[DD].Value.Trim());
                                hotel.ObligatoryService.Add(ExtraSservice);
                                ExtraSservice = new obligatoryService();
                                continue;
                            default:
                                break;
                        }
                    }


                    if (rowData[DD].StyleID == "s79" && rowData[DD].MergeAcross == 4)
                    {
                        if (rowData[DD].Value.Trim().Contains("Staying nights"))
                        {
                            policyItem.StayingNights = rowData[DD].Value.Trim().Replace("Staying nights:  -", "");

                            continue;
                        }
                        else
                        {
                            string periud = rowData[DD].Value.Trim();
                            string[] dates = periud.Split('-');

                            DateTime startDate = DateTime.ParseExact(dates[0].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                            DateTime endDate = DateTime.ParseExact(dates[1].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

                            policyItem.StartDate = startDate;
                            policyItem.EndDate = endDate;

                            LastCaption = string.Empty;

                            continue;
                        }
                    }

                    switch (LastCaption)
                    {
                        case "Limit":

                            LastCaption = string.Empty;
                            continue;
                        case "penalty":
                            if (rowData[DD].StyleID == "s79" && rowData[DD].MergeAcross == 1)
                            {
                                if (policyItem.Penalty == null || policyItem.Penalty == string.Empty)
                                {
                                    policyItem.Penalty += rowData[DD].Value.Trim() + " -> ";
                                }
                                else
                                {
                                    policyItem.Penalty += Environment.NewLine;
                                    policyItem.Penalty += rowData[DD].Value.Trim() + " -> ";
                                }
                            }
                            else if (rowData[DD].StyleID == "s79" && rowData[DD].MergeAcross == 2)
                            {
                                policyItem.Penalty += rowData[DD].Value.Trim();
                            }

                            continue;
                        case "No Show":
                            policyItem.NoShow = rowData[DD].Value.Trim();

                            hotel.Cancellation.Add(policyItem);

                            policyItem = new CancelPolicy();

                            LastCaption = "Cancellation";

                            LastCaption = string.Empty;
                            continue;
                        default:
                            break;
                    }

                    continue;
                }
            }

            if (hotel != null || !string.IsNullOrEmpty(hotel.HotelName))
            {
                hotelList.Add(hotel);
                hotel = null;
            }

            List<ReadyData_7> readyData = new List<ReadyData_7>();

            foreach (var hotelItem in hotelList)
            {
                for (int j = 0; j < hotelItem.Rooms.Count; j++)
                {
                    for (int t = 0; t < hotelItem.Rooms[j].Accommodation.Count; t++)
                    {
                        for (int k = 0; k < hotelItem.Rooms[j].Accommodation[t].Price.Count; k++)
                        {
                            ReadyData_7 data = new ReadyData_7();

                            data.HotelName = hotelItem.HotelName;
                            data.Region = hotelItem.Region;
                            data.Board = hotelItem.Board;
                            data.Category = hotelItem.Category;
                            data.Currency = hotelItem.Currency;
                            data.NightsFrom = hotelItem.NightsFrom;
                            data.NightsTill = hotelItem.NightsTill;

                            data.ReservationStart = hotelItem.Rooms[j].Accommodation[t].Price[k].ReservationStart;
                            data.ReservationEnd = hotelItem.Rooms[j].Accommodation[t].Price[k].ReservationEnd;


                            data.PeriodsStart = hotelItem.Rooms[j].Accommodation[t].Price[k].PeriodsStart;
                            data.PeriodsEnd = hotelItem.Rooms[j].Accommodation[t].Price[k].PeriodsEnd;

                            data.Room = hotelItem.Rooms[j].Room;
                            data.Accommodation = hotelItem.Rooms[j].Accommodation[t].Accommodation;

                            data.Price = hotelItem.Rooms[j].Accommodation[t].Price[k].Price;

                            data.Cancellation = hotelItem.Cancellation;
                            data.ObligatoryService = hotelItem.ObligatoryService;

                            readyData.Add(data);
                        }
                    }
                }
            }

            return readyData.Distinct().ToList();
        }
    }
}