//--- Aura Script -----------------------------------------------------------
//  Goblin Archer Kid AI
//--- Description -----------------------------------------------------------
//  AI for Goblin Archer Kids.
//---------------------------------------------------------------------------

[AiScript("goblinarcherkid")]
public class GoblinArcherKidAi : AiScript
{
    public GoblinArcherKidAi()
    {
        SetAggroRadius(1000); // angle 120 audio 500
        Hates("/pc/", "/pet/");
        SetAggroLimit(AggroLimit.One); // support NONE // also hates when redgoblin is attacked

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
        // <cmd name="switch_weaponset" set="0" />
        Do(Timeout(2000, KeepDistance(800, false)));
        Do(Circle(800, 1000, 1000, false, false));
        var rndAggro = Random();
        if (rndAggro < 30)
        {
            Do(Attack(1, 5000)); // <cmd name="ranged_attack" timeout="5000" />
        }
        else if (rndAggro < 60)
        {
            Do(Attack(1, 5000)); // <cmd name="ranged_attack" timeout="5000" />
            Do(Attack(1, 5000)); // <cmd name="ranged_attack" timeout="5000" />
        }
        else
        {
            // Do(PrepareSkill(SkillId.MagnumShot));
            Do(Attack(1, 5000)); // <cmd name="ranged_attack" timeout="5000" />
        }
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
        if (Random() < 50)
        {
            // <cmd name="switch_weaponset" set="1" />
            Do(PrepareSkill(SkillId.Defense));
            Do(Wait(2000, 4000));
            Do(CancelSkill());
        }
        else
        {
            // <cmd name="switch_weaponset" set="1" />
            Do(PrepareSkill(SkillId.Smash));
            Do(Attack(1, 5000));
        }
    }
}