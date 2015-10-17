//--- Aura Script -----------------------------------------------------------
//  Rat AI
//--- Description -----------------------------------------------------------
//  AI for normal Rats.
//---------------------------------------------------------------------------

[AiScript("rat")]
public class RatAi : AiScript
{
    public RatAi()
    {
        Doubts("/pc/", "/pet/");
        SetAggroRadius(600); // 400 range audio missing  90 angle

        On(AiState.Aggro, AiEvent.DefenseHit, OnDefenseHit);
        On(AiState.Aggro, AiEvent.Hit, OnHit);
        On(AiState.Aggro, AiEvent.KnockDown, OnKnockDown);
    }

    protected override IEnumerable Idle()
    {
        Do(Wander(100, 600));
        Do(Wait(2000, 5000));
    }

    protected override IEnumerable Aggro()
    {
        Do(Wait(4000));
        Do(Attack(3));
        Do(Wait(3000));
    }

    private IEnumerable OnDefenseHit()
    {
        Do(Attack(3));
        Do(Wait(3000));
    }

    private IEnumerable OnHit()
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
        var knockd = Random();
        if (knockd < 30)
        {
            Do(PrepareSkill(SkillId.Defense));
            Do(Say("Grr...")); // _LT[xml.ai.94]
            Do(Wait(4000, 8000));
            Do(CancelSkill());
        }
        else if (knockd < 70)
        {
            Do(Wait(7000, 8000));
        }
        else
        {
            Do(Attack(1, 4000));
        }

    }
}
