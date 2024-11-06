namespace Adnc.Demo.Shared.Rpc.Http.Rtos
{
    public class MessageCreationRto
    {
        public MessageCreationRto(string msgtype, long releaseid, long quoteid, long cid, int ishome, string title, string memo, string thumimg)
        {
            Msgtype = msgtype;
            Releaseid = releaseid;
            Quoteid = quoteid;
            Cid = cid;
            Ishome = ishome;
            Title = title;
            Memo = memo;
            Thumimg = thumimg;
        }

        public string Msgtype { get; set; }

        public long Releaseid { get; set; }

        public long Quoteid { get; set; }

        public long Cid { get; set; }

        public int Ishome { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Memo { get; set; } = string.Empty;

        public string Thumimg { get; set; } = string.Empty;
    }
}
