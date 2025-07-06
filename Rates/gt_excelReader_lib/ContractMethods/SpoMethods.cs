using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace gt_excelReader_lib
{
    public class SpoMethods
    {
        public List<ReadyData_6> Read6_Spo(List<List<CellInfo>> ListData)
        {
            List<hotel_6> hotelList = new List<hotel_6>();
            hotel_6 hotel = new hotel_6();

            string CurrentHeader = string.Empty;

            int priceID = 0;

            int pID = 0;
            int rID = 0;

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
                            hotel = new hotel_6();
                            hotel.HotelName = rowData[DD].Value.Trim();

                            continue;
                        }

                        hotelList.Add(hotel);
                        hotel = new hotel_6();
                        hotel.HotelName = rowData[DD].Value.Trim();

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
                            case "Nights from - till":
                                LastCaption = "Nights from - till"; continue;
                            case "Periods":
                                LastCaption = "Periods"; continue;
                            case "Reservation dates":
                                LastCaption = "Reservation dates"; continue;
                            default:
                                break;
                        }

                        if (LastCaption == "Region") { hotel.Region = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Board") { hotel.Board = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Category") { hotel.Category = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Currency") { hotel.Currency = rowData[DD].Value.Trim(); LastCaption = string.Empty; continue; }
                        if (LastCaption == "Nights from - till")
                        {
                            string r = rowData[DD].Value.Trim();
                            if (r.Contains("-"))
                            {
                                string ft = rowData[DD].Value.Trim();
                                string[] parts = ft.Split('-');
                                if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int start) && int.TryParse(parts[1].Trim(), out int end))
                                {
                                    hotel.NightsFrom = start;
                                    hotel.NightsTill = end;
                                }
                                LastCaption = string.Empty;
                                continue;
                            }
                            else if (int.TryParse(r, out int result))
                            {
                                if (hotel.NightsFrom == 0)
                                {
                                    hotel.NightsFrom = Convert.ToInt32(r);
                                    continue;
                                }
                                else
                                {
                                    hotel.NightsTill = Convert.ToInt32(r);
                                    LastCaption = string.Empty;
                                    continue;
                                }
                            }
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

                    if (rowData[DD].StyleID.Replace("\"", "").ToLower() == "s85".ToLower() || rowData[DD].StyleID.Replace("\"", "").ToLower() == "s65".ToLower())
                    {
                        if (rowData[DD].MergeAcross == 1)
                        {
                            CurrentHeader = "Room";

                            rooms Rooms = new rooms();
                            Rooms.Room = rowData[DD].Value.Trim();

                            hotel.Rooms.Add(Rooms);

                            continue;
                        }
                    }

                    if (hotel.Rooms != null && hotel.Rooms.Count > 0 && rowData[DD].StyleID.Contains("s82"))
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

                    if (rowData[DD].StyleID.Contains("s87") || rowData[DD].StyleID.Contains("s85"))
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
                    if (rowData[DD].Value.Trim().StartsWith("SPO No"))
                    {
                        hotel.SPO_No = rowData[DD].Value.Trim();

                        continue;
                    }
                }
            }

            if (hotel != null || !string.IsNullOrEmpty(hotel.HotelName))
            {
                hotelList.Add(hotel);
                hotel = null;
            }

            List<ReadyData_6> readyData = new List<ReadyData_6>();

            foreach (var hotelItem in hotelList)
            {
                for (int j = 0; j < hotelItem.Rooms.Count; j++)
                {
                    for (int t = 0; t < hotelItem.Rooms[j].Accommodation.Count; t++)
                    {
                        for (int k = 0; k < hotelItem.Rooms[j].Accommodation[t].Price.Count; k++)
                        {
                            ReadyData_6 data = new ReadyData_6();

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

                            data.SPO_No = hotelItem.SPO_No;

                            readyData.Add(data);
                        }
                    }
                }
            }

            return readyData.Distinct().ToList();
        }
    }
}