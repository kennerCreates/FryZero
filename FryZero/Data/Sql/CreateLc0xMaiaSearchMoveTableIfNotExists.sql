create table if not exists Lc0xMaiaSearchMove (
    Id integer primary key autoincrement,
    BookFenBeforeMove text not null,
    VariationNumber integer not null,
    Move text not null,
    NumberOfVisitsToThisNode integer not null,
    NodesSearched integer not null,
    Quality real,
    ProbabilityHumanWouldPlayMove real,
    WinProbability real not null,
    DrawProbability real not null,
    LossProbability real not null,
    CentipawnScore integer not null,
    PreSearchMoveProbabilityEstimate real not null,
    PreSearchEvaluationEstimate real,
    MovesLeftEstimate real,
    PrincipalVariation text not null,
    BookFenAfterMove text not null
)
