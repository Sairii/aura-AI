//--- Aura Script -----------------------------------------------------------
//  Bear AI
//--- Description -----------------------------------------------------------
//  AI for bears.
//---------------------------------------------------------------------------

[AiScript("bear")]
public class BearAi : AiScript
{
    public BearAi()
    {
        SetAggroRadius(700); //90 angle Audio 400
        Doubts("/pc/", "/pet/");
        Hates("/ahchemy_golem/");
        SetAggroDelay(5000);
        SetAggroLimit(AggroLimit.None);

        On(AiState.Aggro, AiEvent.DefenseHit, OnDefenseHit);
        On(AiState.Aggro, AiEvent.Hit, OnHit);
        On(AiState.Aggro, AiEvent.KnockDown, OnKnockDown);
    }

    protected override IEnumerable Idle()
    {
        Do(Wander());
        Do(Wait(2000, 5000));
    }

    protected override IEnumerable Alert()
    {
        var rndalert = Random();
        if (rndalert < 5)
        {
            Do(Attack(3, 4000));
        }
        else if (rndalert < 45)
        {
            if (Random() < 70)
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
            else
            {
                Do(PrepareSkill(SkillId.Counterattack));
                Do(Wait(5000));
                Do(CancelSkill());
            }
        }
        else if (rndalert < 90)
        {
            if (Random() < 55)
            {
                Do(Circle(400, 1000, 4000, true));
            }
            else
            {
                Do(Circle(400, 1000, 4000, false));
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
            var rndagr = Random();
            if (rndagr < 40)
            {
                if (Random() < 50)
                {
                    Do(PrepareSkill(SkillId.Defense));
                    Do(Circle(500, 1000, 6000, true));
                    Do(CancelSkill());
                }
                else
                {
                    Do(PrepareSkill(SkillId.Defense));
                    Do(Circle(500, 1000, 6000, false));
                    Do(CancelSkill());
                }
            }
            else if (rndagr < 70)
            {
                Do(PrepareSkill(SkillId.Smash));
                Do(Attack(1, 5000));
                Do(Wait(3000, 8000));
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
        var onhitr = Random();
        if (onhitr < 15)
        {
            Do(Timeout(2000, KeepDistance(100, true)));
        }
        else if (onhitr < 30)
        {
            Do(Timeout(2000, Wander()));
        }
        else
        {
            Do(Attack(3, 4000));
        }
    }

    private IEnumerable OnKnockDown()
    {
        var knockd = Random();
        if (knockd < 20)
        {
            if (Random() < 50)
            {
                Do(PrepareSkill(SkillId.Defense));
                Do(Follow(100, true, 4000));
                Do(CancelSkill());
            }
            else
            {
                Do(PrepareSkill(SkillId.Defense));
                Do(Timeout(4000, Wander()));
                Do(CancelSkill());
            }
        }
        else if (knockd < 30)
        {
            Do(PrepareSkill(SkillId.Counterattack));
            Do(Wait(2000, 4000));
            Do(CancelSkill());
        }
        else if (knockd < 40)
        {
            Do(PrepareSkill(SkillId.Smash));
            Do(Attack(1, 5000));
            Do(CancelSkill());
        }
        else if (knockd < 70)
        {
            Do(Attack(3, 5000));
        }
        else
        {
            Do(Timeout(500, PrepareSkill(SkillId.Defense)));
            Do(CancelSkill());
            Do(Attack(3, 5000));
        }
    }
}
