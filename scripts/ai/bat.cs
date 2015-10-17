//--- Aura Script -----------------------------------------------------------
//  Bat AI
//--- Description -----------------------------------------------------------
//  AI for Bats.
//---------------------------------------------------------------------------

[AiScript("bat")]
public class BatAi : AiScript
{
    public BatAi()
    {
        Doubts("/pc/", "/pet/");
        SetAggroRadius(200); // 120 angle 1000 audio
        SetAggroLimit(AggroLimit.Two);

        On(AiState.Aggro, AiEvent.DefenseHit, OnDefenseHit);
        On(AiState.Aggro, AiEvent.Hit, OnHit);
    }

    protected override IEnumerable Idle()
    {
        Do(Wander(100, 800));
        Do(Wait(2000, 5000));
    }

    protected override IEnumerable Alert()
    {
        if (Random() < 25)
        {
            Do(PrepareSkill(SkillId.Defense));
        }
        var rndAlert = Random();
        if (rndAlert < 50)
        {
            Do(Circle(300, 1000, 3000, true));
        }
        else
        {
            Do(Circle(300, 1000, 3000, false));
        }
        Do(Wait(2000, 4000));
        Do(CancelSkill());
    }

    protected override IEnumerable Aggro()
    {
        Do(Wait(5000));
        if (Random() < 75)
        {
            var rndAggro = Random();
            if (rndAggro < 22)
            {
                Do(Attack(1, 5000));
            }
            else if (rndAggro < 67)
            {
                Do(Attack(2, 5000));
            }
            else
            {
                Do(Attack(3, 5000));
            }
        }
        else
        {
            Do(PrepareSkill(SkillId.Defense));
        }
        var rndAggro2 = Random();
        if (rndAggro2 < 10)
        {
            Do(Circle(600, 1000, 3000, true));
        }
        else if (rndAggro2 < 20)
        {
            Do(Circle(600, 1000, 3000, false));
        }
        else if (rndAggro2 < 50)
        {
            Do(Timeout(3000, KeepDistance(400, true)));
        }
        else if (rndAggro2 < 70)
        {
            Do(Timeout(3000, KeepDistance(400, false)));
        }
        else
        {
            Do(Wait(3000));
        }
        Do(CancelSkill());
    }

    private IEnumerable OnDefenseHit()
    {
        Do(Attack());
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
}
