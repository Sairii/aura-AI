//--- Aura Script -----------------------------------------------------------
//  Direwolf AI
//--- Description -----------------------------------------------------------
//  AI for direwolf creatures.
//--- Missing ---------------------------------------------------------------
//  Wolf- Support is missing.
//---------------------------------------------------------------------------

[AiScript("direwolf")]
public class DirewolfAi : AiScript
{
    public DirewolfAi()
    {
        Doubts("/pc/", "/pet/", "/cow/");
        Hates("/dog/", "/sheep/");
        //Fear ("/bear/");
        HatesBattleStance(); // needs a 3000 delay
        SetAggroRadius(650); // 400 range audio missing
        SetAggroDelay(6000);
        SetAggroLimit(AggroLimit.None);
        On(AiState.Aggro, AiEvent.DefenseHit, OnDefenseHit);
        On(AiState.Aggro, AiEvent.Hit, OnHit);
    }

    protected override IEnumerable Idle()
    {
        Do(Wander());
        Do(Wait(2000, 5000));
    }

    protected override IEnumerable Alert()
    {
        var rndAlert = Random();
        if (rndAlert < 5)
        {
            Do(Attack(3, 4000));
        }
        else if (rndAlert < 50)
        {
            if (Random() < 60)
            {
                if (Random() < 50)
                {
                    Do(PrepareSkill(SkillId.Defense));
                    Do(Circle(500, 1000, 5000, true));
                    Do(CancelSkill());
                }
                else
                {
                    Do(PrepareSkill(SkillId.Defense));
                    Do(Circle(500, 1000, 5000, false));
                    Do(CancelSkill());
                }
            }
            else // 40%
            {
                Do(PrepareSkill(SkillId.Counterattack));
                Do(Wait(5000));
                Do(CancelSkill());
            }
        }
        else if (rndAlert < 90)
        {
            if (Random() < 55)
            {
                Do(Circle(500, 1000, 5000, true));
            }
            else
            {
                Do(Circle(500, 1000, 5000, false));
            }
        }
        else
        {
            if (Random() < 50)
            {
                Do(Circle(500, 1000, 1000, true, false));
            }
            else
            {
                Do(Circle(500, 1000, 1000, false, false));
            }
        }
    }

    protected override IEnumerable Aggro()
    {
        if (Random() < 50)
        {
            var rndAggro = Random();
            if (rndAggro < 25)
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
            else if (rndAggro < 62)
            {
                Do(PrepareSkill(SkillId.Counterattack));
                Do(Wait(5000, 5000));
                Do(CancelSkill());
            }
            else
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
            Do(Wander());
        }
        else
        {
            Do(Attack(3));
            Do(Wait(4000, 4000));
        }
    }
}
