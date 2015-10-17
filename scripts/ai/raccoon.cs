//--- Aura Script -----------------------------------------------------------
//  Raccoon AI
//--- Description -----------------------------------------------------------
//  AI for normal Raccoon-like creatures.
//---------------------------------------------------------------------------

[AiScript("raccoon")]
public class RaccoonAi : AiScript
{
    public RaccoonAi()
    {
        Doubts("/pc/", "/pet/");
        Hates("/chicken/");
        SetAggroRadius(600); // 400 range audio missing

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
        if (Creature.Skills.Has(SkillId.Defense) && (Random() < 25)) // 25%
        {
            Do(PrepareSkill(SkillId.Defense));
            Do(Wait(2000, 4000));
            Do(CancelSkill());
        }
        else // 75%
        {
            var rndalert = Random();
            Do(Wait(2000, 4000));
            if (rndalert < 25)
                Do(Circle(300, 800, 800, true));
            else if (rndalert < 50)
                Do(Circle(300, 800, 800, false));
            else if (rndalert < 75)
                Do(Timeout(800, KeepDistance(500, true)));
            else
                Do(Timeout(800, Follow(300)));

        }
    }

    protected override IEnumerable Aggro()
    {
        if (Random() < 90)
        {
            var rndaggro = Random();
            Do(Wait(1000, 1000));
            if (rndaggro < 60)
                Do(Attack(1, 4000));
            else if (rndaggro < 80)
                Do(Attack(2, 4000));
            else
                Do(Attack(3, 4000));
        }
        else
        {
            var rndaggro2 = Random();
            Do(Wait(1000, 1000));
            if (rndaggro2 < 10)
                Do(Circle(300, 800, 800, true));
            else if (rndaggro2 < 20)
                Do(Circle(300, 800, 800, false));
            else if (rndaggro2 < 45)
                Do(Timeout(800, KeepDistance(300, true)));
            else if (rndaggro2 < 70)
                Do(Timeout(800, Follow(100)));
            else
                Do(Wait(2000, 3000));
        }
    }

    private IEnumerable OnDefenseHit()
    {
        Do(Wait(1000, 1000));
        Do(Attack(3));
        Do(Wait(3000, 3000));
    }

    private IEnumerable OnHit()
    {
        Do(Wait(1000, 2000));
    }

    private IEnumerable OnKnockDown()
    {
        var rndOKD = Random();
        if (rndOKD < 10)
            if (Creature.Skills.Has(SkillId.Defense))
            {
                Do(PrepareSkill(SkillId.Defense));
                Do(Say("...")); // "say_to_all" text="_LT[xml.ai.421]"
                Do(Wait(4000, 8000));
                Do(CancelSkill());
            }
            else
            {
                Do(Wait(4000, 8000));
            }
        else if (rndOKD < 60)
            Do(Wait(7000, 8000));
        else
            Do(Wait(1000, 1000));
        Do(Attack(1, 4000));
    }
}
