//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BSE.Tunes.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class AlbumEntity
    {
        public int TitelID { get; set; }
        public int InterpretID { get; set; }
        public string Titel1 { get; set; }
        public Nullable<int> ErschDatum { get; set; }
        public Nullable<int> MediumID { get; set; }
        public Nullable<int> mp3tag { get; set; }
        public string Guid { get; set; }
        public string PictureFormat { get; set; }
        public byte[] Cover { get; set; }
        public byte[] thumbnail { get; set; }
        public Nullable<System.DateTime> ErstellDatum { get; set; }
        public string ErstellerNm { get; set; }
        public Nullable<System.DateTime> MutationDatum { get; set; }
        public string MutationNm { get; set; }
        public System.DateTime Timestamp { get; set; }
        public Nullable<int> genreId { get; set; }
    }
}
