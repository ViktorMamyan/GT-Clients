using System;
using System.Text.RegularExpressions;

namespace gt_excelReader_lib
{
    public static class Parser6
    {
        public static ParseObject ParseX(string Accommodation)
        {
            Accommodation = Accommodation.Replace("ON EXB", "");
            Accommodation = Accommodation.Replace("ON SHARED EXB", "");

            ParseObject parseObject = Parse1(Accommodation);

            if (parseObject == null || parseObject.ADL == 0) parseObject = Parse2(Accommodation);

            return parseObject;
        }

        private static ParseObject Parse1(string Accommodation)
        {
            Accommodation = Regex.Replace(Accommodation, "ON EXB", "", RegexOptions.None);
            Accommodation = Regex.Replace(Accommodation, "ON SHARED EXB", "", RegexOptions.None);

            ParseObject parseObject = new ParseObject();

            ParseData data = new ParseData();

            var adlMatch = Regex.Match(Accommodation, @"(\d+)\s+ADL");
            if (adlMatch.Success)
                data.ADL = int.Parse(adlMatch.Groups[1].Value);

            var chdMatch = Regex.Match(Accommodation, @"(\d+)\s+CHD");
            if (chdMatch.Success)
            {
                int chdCount = int.Parse(chdMatch.Groups[1].Value);
                data.CHD = chdCount;
            }

            if (data == null || data.ADL == 0) return null;

            parseObject.ADL = data.ADL;
            parseObject.CHD = data.CHD;

            var ageMatches = Regex.Matches(Accommodation, @"\((\d{1,2}-\d{1,2}\.\d{1,2})\)");
            if (ageMatches.Count > 0)
            {
                if (ageMatches.Count >= 1)
                {
                    var rangeParts = ageMatches[0].Groups[1].Value.Split('-');
                    double chd1Start = double.Parse(rangeParts[0]);
                    double chd1End = double.Parse(rangeParts[1]);

                    parseObject.CHDStart1 = (int)Math.Ceiling(chd1Start);
                    parseObject.CHDEnd1 = (int)Math.Ceiling(chd1End);

                    if (parseObject.CHDStart1.HasValue && parseObject.CHDStart1.Value > 0)
                    {
                        parseObject.INFStart = 0;
                        parseObject.INFEnd = parseObject.CHDStart1.Value;
                    }
                    else if (parseObject.CHDStart1.HasValue == true && parseObject.CHDStart1.Value == 0)
                    {
                        parseObject.INFStart = 0;
                        parseObject.INFEnd = parseObject.CHDEnd1.Value;
                    }
                }

                if (ageMatches.Count >= 2)
                {
                    var rangeParts2 = ageMatches[1].Groups[1].Value.Split('-');
                    double chd2Start = double.Parse(rangeParts2[0]);
                    double chd2End = double.Parse(rangeParts2[1]);

                    parseObject.CHDStart2 = (int)Math.Ceiling(chd2Start);
                    parseObject.CHDEnd2 = (int)Math.Ceiling(chd2End);
                }

            }

            return parseObject;
        }

        private static ParseObject Parse2(string Accommodation)
        {
            Accommodation = Regex.Replace(Accommodation, "EXB", "", RegexOptions.None);

            ParseObject parseObject = new ParseObject();
            ParseData data = new ParseData();

            var adlMatch = Regex.Match(Accommodation, @"(?<adl>\d+)\s*AD[L]?");
            if (adlMatch.Success)
            {
                data.ADL = int.Parse(adlMatch.Groups["adl"].Value);
                parseObject.ADL = data.ADL;
            }

            if (data == null || data.ADL == 0) return null;

            var chdMatch = Regex.Match(Accommodation, @"(?<chd>\d+)\s*CHD");
            if (chdMatch.Success)
            {
                data.CHD = int.Parse(chdMatch.Groups["chd"].Value);
                parseObject.CHD = data.CHD;
            }

            var ageMatches = Regex.Matches(Accommodation, @"\((\d{1,2}-\d{1,2}(?:\.\d{1,2})?)\)");
            if (ageMatches.Count > 0)
            {
                if (ageMatches.Count >= 1)
                {
                    var rangeParts = ageMatches[0].Groups[1].Value.Split('-');
                    double chd1Start = double.Parse(rangeParts[0]);
                    double chd1End = double.Parse(rangeParts[1]);

                    parseObject.CHDStart1 = (int)Math.Ceiling(chd1Start);
                    parseObject.CHDEnd1 = (int)Math.Ceiling(chd1End);

                    if (parseObject.CHDStart1.HasValue && parseObject.CHDStart1.Value > 0)
                    {
                        parseObject.INFStart = 0;
                        parseObject.INFEnd = parseObject.CHDStart1.Value;
                    }
                    else if (parseObject.CHDStart1.HasValue == true && parseObject.CHDStart1.Value == 0)
                    {
                        parseObject.INFStart = 0;
                        parseObject.INFEnd = parseObject.CHDEnd1.Value;
                    }
                }

                if (ageMatches.Count >= 2)
                {
                    var rangeParts2 = ageMatches[1].Groups[1].Value.Split('-');
                    double chd2Start = double.Parse(rangeParts2[0]);
                    double chd2End = double.Parse(rangeParts2[1]);

                    parseObject.CHDStart2 = (int)Math.Ceiling(chd2Start);
                    parseObject.CHDEnd2 = (int)Math.Ceiling(chd2End);
                }

                if (ageMatches.Count >= 3)
                {
                    var rangeParts3 = ageMatches[2].Groups[1].Value.Split('-');
                    double chd3Start = double.Parse(rangeParts3[0]);
                    double chd3End = double.Parse(rangeParts3[1]);

                    parseObject.CHDStart3 = (int)Math.Ceiling(chd3Start);
                    parseObject.CHDEnd3 = (int)Math.Ceiling(chd3End);
                }
            }

            return parseObject;
        }
    }
}