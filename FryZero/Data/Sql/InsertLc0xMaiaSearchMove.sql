insert into
    Lc0xMaiaSearchMove (
        BookFenBeforeMove,
        VariationNumber,
        Move,
        NumberOfVisitsToThisNode,
        NodesSearched,
        Quality,
        ProbabilityHumanWouldPlayMove,
        WinProbability,
        DrawProbability,
        LossProbability,
        CentipawnScore,
        PreSearchMoveProbabilityEstimate,
        PreSearchEvaluationEstimate,
        MovesLeftEstimate,
        PrincipalVariation,
        BookFenAfterMove
    )
select
    @BookFenBeforeMove,
    @VariationNumber,
    @Move,
    @NumberOfVisitsToThisNode,
    @NodesSearched,
    @Quality,
    @ProbabilityHumanWouldPlayMove,
    @WinProbability,
    @DrawProbability,
    @LossProbability,
    @CentipawnScore,
    @PreSearchMoveProbabilityEstimate,
    @PreSearchEvaluationEstimate,
    @MovesLeftEstimate,
    @PrincipalVariation,
    @BookFenAfterMove
where not exists
(
    select
        1
    from
        Lc0xMaiaSearchMove
    where
        BookFenBeforeMove = @BookFenBeforeMove
        and Move = @Move
)
