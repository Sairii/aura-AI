//--- Aura Script -----------------------------------------------------------
//  Field Spider AI
//--- Description -----------------------------------------------------------
//  AI for Field Spiders.
//---------------------------------------------------------------------------

[AiScript("field_spider")]
public class Field_SpiderAi : AiScript
{
    public Field_SpiderAi()
    {
        Doubts("/pc/", "/pet/");
        SetAggroRadius(600); // 45 angle 400 audio
        SetAggroLimit(AggroLimit.One); // Support One

        On(AiState.Aggro, AiEvent.DefenseHit, OnDefenseHit);
        On(AiState.Aggro, AiEvent.Hit, OnHit);
    }

    protected override IEnumerable Idle()
    {
        Do(Timeout(5000, Wander(100, 400)));
        var rndIdle = Random();
        if (rndIdle < 22) // 22%
        {
            if (Random() < 1) // 1%
            {
                if (Random() < 10) // 0,1%
                {
                    if (Creature.Mana > 200)
                    {
                        for (int i = 1; i <= 20; i++)
                        {
                            Do(Timeout(500, PrepareSkill(SkillId.WebSpinning)));
                            Do(Wait(500));
                        }
                        for (int i = 1; i <= 5; i++)
                        {
                            if (Random() < 50)
                            {
                                Do(Timeout(500, PrepareSkill(SkillId.WebSpinning)));
                                Do(Wait(500));
                            }
                        }
                    }
                }
            }
            else // ~22%
            {
                Do(Timeout(500, PrepareSkill(SkillId.WebSpinning)));
            }
        }
        Do(Wait(2000, 5000));
    }

    protected override IEnumerable Alert()
    {
        Do(Follow(400, true));
    }

    protected override IEnumerable Aggro()
    {
        if (Random() < 33)
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
