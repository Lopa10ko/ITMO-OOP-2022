namespace Banks.Entities;

public class DepositRules
{
    private List<DepositRule> _depositRules;

    private DepositRules(List<DepositRule> rules)
    {
        MaximumPercent = new ValueAmount(rules.Max(rule => rule.Percent.Value));
        _depositRules = rules.OrderBy(rule => rule.CriticalValue.Value).ToList();
    }

    public static DepositRulesBuilder Builder => new DepositRulesBuilder();
    public IReadOnlyList<DepositRule> Rules => _depositRules.AsReadOnly();
    public ValueAmount MaximumPercent { get; }

    public class DepositRulesBuilder
    {
        private readonly List<DepositRule> _rules = new List<DepositRule>();

        public DepositRules Build()
        {
            return new DepositRules(_rules);
        }

        public DepositRulesBuilder AddDepositRule(DepositRule rule)
        {
            if (_rules.Contains(rule))
                return this;
            bool overlapping = _rules.Any(r => r.IsOverlapping(rule));
            if (overlapping)
            {
                DepositRule overlappingRule = _rules.Single(r => r.IsOverlapping(rule));
                rule = rule.GetMaximumPercent(overlappingRule);
            }

            _rules.Add(rule);
            return this;
        }
    }
}