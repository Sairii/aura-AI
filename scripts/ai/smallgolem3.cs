//--- Aura Script -----------------------------------------------------------
//  Small Golem AI
//--- Description -----------------------------------------------------------
//  AI for Ciar Beginner Golem (smallgolem3 130014)
//---------------------------------------------------------------------------

[AiScript("smallgolem3")]
public class SmallGolem3Ai : AiScript
{
    public SmallGolem3Ai()
    {
        SetAggroRadius(1500); // angle 90 audio 1200
        Hates("/pc/", "/pet/");

        On(AiState.Aggro, AiEvent.DefenseHit, OnDefenseHit);
        On(AiState.Aggro, AiEvent.Hit, OnHit);
        On(AiState.Aggro, AiEvent.KnockDown, OnKnockDown);
    }

    protected override IEnumerable Idle()
    {
        Do(StartSkill(SkillId.Rest));
        Do(Wait(1000000000));
    }

    protected override IEnumerable Aggro()
    {
        var rndAggro = Random();
        if (rndAggro < 50)
        {
            if (Random() < 20)
            {
                Do(PrepareSkill(SkillId.Smash));
                Do(Attack(1, 4000));
            }
            else
            {
                Do(Attack(3, 4000));
            }
        }
        else if (rndAggro < 70)
        {
            Do(PrepareSkill(SkillId.Defense));
            Do(Follow(600, true, 5000));
            Do(CancelSkill());
        }
        else if (rndAggro < 90)
        {
            Do(PrepareSkill(SkillId.Stomp));
            Do(Wait(500, 2000));
            Do(UseSkill());
            // <cmd name="process_skill" target="2" />
            // Do(CancelSkill()); does not have a cancel
            Do(Wait(1500));
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
        if (Creature.Life < Creature.LifeMax * 0.50f && Creature.Life > Creature.LifeMax * 0.30f)
        {
            Do(Say("_LT[xml.ai.523]"));
            Do(Say("_LT[xml.ai.524]"));
            Do(Say("_LT[xml.ai.525]"));
            Do(Say("_LT[xml.ai.526]"));
        }

        if (Creature.Life < Creature.LifeMax * 0.30f)
        {
            var rndOKD = Random();
            if (rndOKD < 20)
            {
                Do(PrepareSkill(SkillId.Stomp));
		Do(UseSkill());
                // <cmd name="process_skill" target="2" />
                Do(Wait(1500));
            }
            else if (rndOKD < 25)
            {
                Do(PrepareSkill(SkillId.Smash));
                Do(Attack(1, 4000));
            }
            else if (rndOKD < 35)
            {
                Do(PrepareSkill(SkillId.Defense));
                Do(Wait(2000, 4000));
                Do(CancelSkill());
            }
        }
        else
        {
            var rndOKD2 = Random();
            if (rndOKD2 < 20)
            {
                Do(PrepareSkill(SkillId.Windmill));
                Do(Wait(4000));
                // <cmd name="process_skill" target="2" />
		Do(UseSkill());
            }
            else if (rndOKD2 < 25)
            {
                Do(PrepareSkill(SkillId.Smash));
                Do(Attack(1, 4000));
            }
            else if (rndOKD2 < 35)
            {
                Do(PrepareSkill(SkillId.Defense));
                Do(Wait(2000, 4000));
                Do(CancelSkill());
            }
        }
    }
}