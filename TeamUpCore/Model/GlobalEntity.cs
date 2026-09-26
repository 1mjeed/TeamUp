namespace TeamUpCore.Model
{
    public abstract class GlobalEntity
    {
        #region  Proprities
        /// <summary>
        /// Created Date 
        /// </summary>
        public DateTime CREATEDAT { get; set; }

        /// <summary>
        ///  Updated Date 
        /// </summary>
        public DateTime? UPDATEDAT { get; set; }

        /// <summary>
        ///  Deleted Date 
        /// </summary>
        public DateTime? DELETEDAT { get; set; }

        /// <summary>
        /// User Created Row 
        /// </summary>
        public int CREATEDBY { get; set; }

        /// <summary>
        /// User Updated Row 
        /// </summary>
        public int? UPDATEDBY { get; set; }

        /// <summary>
        /// User Deleted Row 
        /// </summary>
        public int? DELETEDBY { get; set; }

        /// <summary>
        /// Is row deleted
        /// </summary>
        public bool ISDELETED { get; set; }
        #endregion
    }
}
