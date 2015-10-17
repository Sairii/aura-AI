//--- Aura Script -----------------------------------------------------------
//  Fox AI
//--- Description -----------------------------------------------------------
//  AI for foxes and fox-like creatures.
//---------------------------------------------------------------------------

[AiScript("fox")]
public class FoxAi : AiScript
{
    public FoxAi()
    {
        Doubts("/pc/", "/pet/");
        Hates("/chicken/");
        SetAggroRadius(600); // 90 angle 400 audio

        On(AiState.Aggro, AiEvent.DefenseHit, OnDefenseHit);
        On(AiState.Aggro, AiEvent.KnockDown, OnKnockDown);
        On(AiState.Aggro, AiEvent.Hit, OnHit);
    }

    protected override IEnumerable Idle()
    {
        Do(Wander(100, 300));
        Do(Wait(2000, 5000));
    }

    protected override IEnumerable Alert()
    {
        if (Random() < 25)
        {
            Do(PrepareSkill(SkillId.Defense));
        }
        var rndAlert = Random();
        if (rndAlert < 15)
        {
            Do(Circle(300, 1000, 5000, true));
        }
        else if (rndAlert < 30)
        {
            Do(Circle(300, 1000, 5000, false));
        }
        else
        {
            Do(Timeout(3000, KeepDistance(400, true)));
        }
        Do(Wait(2000, 4000));
        Do(CancelSkill());
    }

    protected override IEnumerable Aggro()
    {
        if (Random() < 75)
        {
            var rndAggro = Random();
            if (rndAggro < 20)
            {
                if (Creature.Skills.Has(SkillId.Smash) && (Creature.Stamina > 10))
                {
                    Do(PrepareSkill(SkillId.Smash));
                    Do(Attack(1, 6000));
                }
                else
                {
                    if (Random() < 56)
                    {
                        Do(Attack(1, 5000));
                    }
                    else
                    {
                        Do(Attack(2, 5000));
                    }
                }
            }
            else if (rndAggro < 40)
            {
                if (Creature.Skills.Has(SkillId.Counterattack) && (Creature.Stamina > 10))
                {
                    Do(PrepareSkill(SkillId.Counterattack));
                    Do(Wait(5000));
                    Do(CancelSkill());
                }
                else
                {
                    if (Random() < 56)
                    {
                        Do(Attack(1, 5000));
                    }
                    else
                    {
                        Do(Attack(2, 5000));
                    }
                }
            }
            else
            {
                if (Random() < 56)
                {
                    Do(Attack(1, 5000));
                }
                else
                {
                    Do(Attack(2, 5000));
                }
            }
        }
        else
        {
            Do(PrepareSkill(SkillId.Defense));
            if (Random() < 50)
            {
                Do(Circle(400, 1000, 3000, true));
            }
            else
            {
                Do(Circle(400, 1000, 3000, true));
            }
        }
        var rndAggro2 = Random();
        if (rndAggro2 < 15)
        {
            Do(Circle(400, 1000, 3000, true));
        }
        else if (rndAggro2 < 30)
        {
            Do(Circle(400, 1000, 3000, false));
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
        Do(Wait(1000, 3000));
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

    private IEnumerable OnKnockDown()
    {
        var knockd = Random();
        if (knockd < 10)
        {
            Do(Say("Grr...")); // _LT[xml.ai.94]
            Do(PrepareSkill(SkillId.Defense));
            if (Random() < 50)
            {
                Do(Circle(400, 1000, 3000, true));
            }
            else
            {
                Do(Circle(400, 1000, 3000, false));
            }
            Do(CancelSkill());
        }
        else if (knockd < 20)
        {
            Do(Wait(2000, 4000));
        }
        else
        {
            Do(Wait(2000, 3000));
            Do(Attack(1, 4000));
        }

    }
}
