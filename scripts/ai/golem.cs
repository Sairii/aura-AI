//--- Aura Script -----------------------------------------------------------
//  Golem AI
//--- Description -----------------------------------------------------------
//  AI for golems.
//---------------------------------------------------------------------------

[AiScript("golem")]
public class GolemAi : AiScript
{
	public GolemAi()
	{
		Hates("/pc/", "/pet/");
		
		On(AiState.Aggro, AiEvent.DefenseHit, OnDefenseHit);
	}

	protected override IEnumerable Idle()
	{
		Do(StartSkill(SkillId.Rest));
		Do(Wait(1000000000));
	}
	
	protected override IEnumerable Aggro()
	{
		var rndAggro = Random();
        if (rndAggro < 40)
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
		else if (rndAggro < 50)
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
            Do(Wait(1500));
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

    private IEnumerable OnKnockDown()
    {

        if (Creature.Life < Creature.LifeMax * 0.30f)
        {
            var rndOKD = Random();
            if (rndOKD < 30)
            {
                Do(PrepareSkill(SkillId.Stomp));
				Do(UseSkill());
                Do(Wait(1500));
            }
            else if (rndOKD < 35)
            {
                Do(PrepareSkill(SkillId.Smash));
                Do(Attack(1, 4000));
            }
            else if (rndOKD < 45)
            {
                Do(PrepareSkill(SkillId.Defense));
                Do(Wait(2000, 4000));
                Do(CancelSkill());
            }
        }
        else
        {
            var rndOKD2 = Random();
            if (rndOKD2 < 40)
            {
                Do(PrepareSkill(SkillId.Windmill));
                Do(Wait(4000));
				Do(UseSkill());
            }
            else if (rndOKD2 < 45)
            {
                Do(PrepareSkill(SkillId.Smash));
                Do(Attack(1, 4000));
            }
            else if (rndOKD2 < 55)
            {
                Do(PrepareSkill(SkillId.Defense));
                Do(Wait(2000, 4000));
                Do(CancelSkill());
            }
        }
    }
}