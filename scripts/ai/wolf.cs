//--- Aura Script -----------------------------------------------------------
//  Wolf AI
//--- Description -----------------------------------------------------------
//  AI for wolves.
//--- Missing ---------------------------------------------------------------
//  Wolf- Support is missing.
//---------------------------------------------------------------------------

[AiScript("wolf")]
public class WolfAi : AiScript
{
    public WolfAi()
    {
        SetAggroRadius(650); // audio 400
        Doubts("/pc/", "/pet/");
        Doubts("/cow/");
        Hates("/sheep/");
        Hates("/dog/");
        HatesBattleStance(); // 3000 delay

        On(AiState.Aggro, AiEvent.DefenseHit, OnDefenseHit);
        On(AiState.Aggro, AiEvent.Hit, OnHit);
    }

    protected override IEnumerable Idle()
    {
        Do(Wander(100, 400));
        Do(Wait(2000, 5000));
    }

    protected override IEnumerable Alert()
    {
        if (Random() < 50)
        {
            if (Random() < 50)
            {
                Do(PrepareSkill(SkillId.Defense));
                Do(Circle(500, 1000, 5000));
                Do(CancelSkill());
            }
            else
            {
                Do(PrepareSkill(SkillId.Counterattack));
                Do(Wait(5000));
                Do(CancelSkill());
            }
        }
        else
        {
            if (Random() < 50)
            {
                Do(Circle(500, 1000, 5000, true));
            }
            else
            {
                Do(Circle(500, 1000, 5000, false));
            }
        }
    }

    protected override IEnumerable Aggro()
    {
        if (Random() < 50)
        {
            var rndnum = Random();
            if (rndnum < 25) // 25%
            {
                if (Random() < 50)
                {
                    Do(PrepareSkill(SkillId.Defense));
                    Do(Circle(400, 1000, 5000, true));
                    Do(CancelSkill());
                }
                else
                {
                    Do(PrepareSkill(SkillId.Defense));
                    Do(Circle(400, 1000, 5000, false));
                    Do(CancelSkill());
                }
            }
            else if (rndnum < 62) // 37%
            {
                Do(PrepareSkill(SkillId.Counterattack));
                Do(Wait(5000));
                Do(CancelSkill());
            }
            else // 38%
            {
                Do(PrepareSkill(SkillId.Smash));
                Do(Attack(1, 5000));
            }
        }
        else
        {
            Do(Attack(3, 5000));
        }
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
            Do(Timeout(2000, KeepDistance(1000, true)));
        }
        else if (rndOH < 30)
        {
            Do(Timeout(2000, Wander()));
        }
        else
        {
            Do(Attack(3));
            Do(Wait(4000, 4000));
        }
    }
}
