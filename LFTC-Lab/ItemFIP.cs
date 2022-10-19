namespace LFTC_Lab
{
    public record ItemFIP
    {
        public int TypeID { get; set; }
        public int SymbolID { get; set; }

        public ItemFIP()
        {
            SymbolID = -1;
        }
    }
}
