//--- Aura Script -----------------------------------------------------------
//  Goblin Kid AI
//--- Description -----------------------------------------------------------
//  AI for Goblin Kids and Poison Goblin Kids.
//---------------------------------------------------------------------------

[AiScript("goblinkid")]
public class GoblinKidAi : AiScript
{
    public GoblinKidAi()
    {
        SetAggroRadius(600); // angle 120 audio 200
        Hates("/pc/", "/pet/");
        SetAggroLimit(AggroLimit.One); // support NONE

        On(AiState.Aggro, AiEvent.DefenseHit, OnDefenseHit);
        On(AiState.Aggro, AiEvent.Hit, OnHit);
        On(AiState.Aggro, AiEvent.KnockDown, OnKnockDown);
    }

    protected override IEnumerable Idle()
    {
        Do(Wander(300, 500));
        Do(Wait(2000, 5000));
    }

    protected override IEnumerable Aggro()
    {
        Do(CancelSkill());
        var rndAggro = Random();
        if (rndAggro < 60)
        {
            Do(Wait(1000, 3000));
            // <cmd name="switch_weaponset" set="2" />
            Do(Attack(3, 5000));
        }
        else if (rndAggro < 75)
        {
            // <cmd name="say_to_all" text="_LT[xml.ai.995]" />
            Do(PrepareSkill(SkillId.Smash));
            Do(Wander(300, 2000, false));
            Do(Attack(1, 5000));
        }
        else if (rndAggro < 90)
        {
            Do(PrepareSkill(SkillId.Defense));
            Do(Circle(5000, 1000, 2000, false, false));
            Do(CancelSkill());
        }
        else
        {
            // <cmd name="say_to_all" text="_LT[xml.ai.996]" />
            Do(PrepareSkill(SkillId.Counterattack));
            Do(Wait(3000, 5000));
            Do(CancelSkill());
        }
        Do(Wait(2000, 3000));
    }

    private IEnumerable OnDefenseHit()
    {
        Do(Attack());
        Do(Wait(3000));
    }

    private IEnumerable OnHit() // from general patterns
    {
        var rndOH = Random();
        if (rndOH < 15)
        {
            Do(Timeout(2000, KeepDistance(1000, false)));
        }
        else if (rndOH < 30)
        {
            Do(Timeout(2000, Wander(100, 500, false)));
        }
        else
        {
            Do(Attack(3, 4000));
        }
    }

    private IEnumerable OnKnockDown()
    {
        Do(Wander(300, 500));
        Do(Attack());
        Do(Wait(2000, 5000));
    }
}