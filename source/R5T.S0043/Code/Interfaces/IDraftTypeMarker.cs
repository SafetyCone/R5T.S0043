using System;


namespace R5T.S0043.T000
{
    /// <summary>
    /// Marker interface for draft types (types that should be moved to a permanent library location, or improved).
    /// Allows methods-for, and extension methods-on, all types that are draft types.
    /// </summary>
    [DraftTypeMarker] // Marked itself!
    public interface IDraftTypeMarker // : IDraftTypeMarker, cannot mark itself. Inherited interface 'IDraftTypeMarker' causes a cycle in the interface hierarchy of 'IDraftTypeMarker'
    {
    }
}
