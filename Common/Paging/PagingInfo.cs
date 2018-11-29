using System;

namespace Common.Paging
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
    public class PagingInformation
    {
        public int RequestPage { get; set; }
        public bool CanPaging { get; set; }
        public int CurrentPage { get; set; }
        public int NumberOfRecords { get; set; }
        public int RecordsPerPage { get; set; }
        public int NumberOfPages
        {
            get
            {
                SetDefault();
                return (int)Math.Ceiling((decimal)NumberOfRecords / RecordsPerPage);
            }
        }
        public int StartNumber
        {
            get
            {
                SetDefault();
                return (CurrentPage - 1) * NumberOfRecords + 1;
            }
        }
        public int NumberOfSkipItem
        {
            get
            {
                SetDefault();
                return StartNumber - 1;
            }
        }
        public int ViewableRecord
        {
            get
            {
                if (CurrentPage < NumberOfPages) return RecordsPerPage;
                return NumberOfRecords - NumberOfSkipItem;
            }
        }
        public int EndNumber
        {
            get
            {
                SetDefault();
                return StartNumber + (RecordsPerPage > ViewableRecord ? ViewableRecord : RecordsPerPage);
            }
        }
        public string MessageText { get; set; }
        void SetDefault()
        {
            if (RecordsPerPage < 1)
                RecordsPerPage = 10;
            if (CurrentPage < 0 || CurrentPage > NumberOfPages) CurrentPage = 1;
        }
    }
    public class Paging
    {
        public int TotalRecord { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string OrderDirection { get; set; }
    }
}
