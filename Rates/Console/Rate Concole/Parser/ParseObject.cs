namespace ConsoleApp1
{
    public class ParseObject
    {
        public string Accommodation { get; set; }
        public int ADL { get; set; }
        public int CHD { get; set; }

        public int? CHDStart1 { get; set; }
        public int? CHDEnd1 { get; set; }

        public int? CHDStart2 { get; set; }
        public int? CHDEnd2 { get; set; }

        public int? CHDStart3 { get; set; }
        public int? CHDEnd3 { get; set; }

        public int? INFStart { get; set; }
        public int? INFEnd { get; set; }
    }

    public class ParseData
    {
        public int ADL { get; set; }
        public int CHD { get; set; }
        public string AgeRange1 { get; set; }
        public int CHDAgeCount { get; set; }
        public string AgeRange2 { get; set; }

        public override string ToString() => $"ADL: {ADL}, CHD1: {CHD} ({AgeRange1}), CHD2: {CHDAgeCount} ({AgeRange2})";
    }

}