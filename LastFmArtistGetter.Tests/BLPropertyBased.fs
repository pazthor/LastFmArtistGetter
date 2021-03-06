﻿namespace LastFmArtistGetter.Tests

module BLPropertyBased = 

    open LastFmArtistGetter
    open FsCheck.Xunit
    open ArrayHelpers

    let ``pairs from collection should be ordered`` orderFn artists =
        let orderedArtists = artists |> orderFn
        match orderedArtists with
        | Success s -> s |> Array.pairwise |> Array.forall (fun (x,y) -> x.listeners >= y.listeners)
        | Failure _ -> false
    
    [<Property>]
    let pairwise x =
        ``pairs from collection should be ordered`` orderArtistsByListenersCount x

    let ``should be permutation of original elements`` orderFn artists =
        let orderedArtists = artists |> orderFn
        match orderedArtists with
        | Success s -> s |> List.ofArray |> isPermutationOf (List.ofArray artists)
        | Failure _ -> false
        
    [<Property>]
    let isPermutation x =
        ``should be permutation of original elements`` orderArtistsByListenersCount x
    
