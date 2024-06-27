namespace TMSQPWeb.ViewModels
{
    public class PayslipImportDetailViewModel
    {
        public PayslipImportDetailViewModel() { }


        public PayslipImportDetailViewModel(byte _detailTypeNo) 
        {
            DetailTypeNo = _detailTypeNo;
        }


        public PayslipImportDetailViewModel(short _piNo, short _headerNo, short _detailNo, byte _detailTypeNo, int _rowNo, string _itemName, decimal? _amount)
        {
            PINo = _piNo;
            HeaderNo = _headerNo;
            DetailNo = _detailNo;
            RowNo = ++_rowNo;
            DetailTypeNo = _detailTypeNo;
            ItemName = _itemName;
            Amount = _amount;
        }

        public short PINo { get; set; }

        public short HeaderNo { get; set; }

        public short DetailNo { get; set; }





        public int RowNo { get; set; }

        public byte DetailTypeNo { get; set; }

        public string ItemName { get; set; }    

        public decimal? Amount { get; set; }
    }


}
