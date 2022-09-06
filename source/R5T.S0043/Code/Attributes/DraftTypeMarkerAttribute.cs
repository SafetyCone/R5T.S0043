using System;


namespace R5T.S0043.T000
{
    /// <summary>
    /// Attribute indicating a type is a draft type (can be either an interface, or a class).
    /// The marker attribute is useful for surveying for draft types and building a catalogue of draft types as a kind of TODO list.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    [DraftTypeMarker] // Ha! Marked itself!
    public class DraftTypeMarkerAttribute : Attribute, IDraftTypeMarker // Ha, marked itself!
    {
        private readonly bool zIsDraftType;
        /// <summary>
        /// Allows specifying that a type is *not* a draft type.
        /// This is useful for marking types that end up canonical draft type file locations, but are not actually draft types.
        /// </summary>
        public bool IsDraftType
        {
            get
            {
                return this.zIsDraftType;
            }
        }


        public DraftTypeMarkerAttribute(
            bool isDraftType = true)
        {
            this.zIsDraftType = isDraftType;
        }
    }
}
