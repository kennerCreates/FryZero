delete from
    Lc0xMaiaSearchMove
where
    BookFenBeforeMove = @BookFenBeforeMove
    and NodesSearched < @NodesSearched
