//--- Aura Script -----------------------------------------------------------
//  Mimic AI
//--- Description -----------------------------------------------------------
//  AI for Mimic.
//---------------------------------------------------------------------------

[AiScript("mimic")]
public class MimicAi : AiScript
{
    public MimicAi()
    {
        Doubts("/pc/", "/pet/");
        SetAggroRadius(400); // 270 angle 120 audio

        On(AiState.Aggro, AiEvent.DefenseHit, OnDefenseHit);
        On(AiState.Aggro, AiEvent.Hit, OnHit);
    }

    protected override IEnumerable Idle()
    {
        Do(Wait(1000000000));
    }

    protected override IEnumerable Aggro()
    {
        if (Random() < 75)
        {
            Do(Attack(3, 4000));
        }
        else
        {
            Do(PrepareSkill(SkillId.Defense));
        }
        if (Random() < 50)
        {
            Do(Circle(500, 1000, 3000, true, false));
        }
        else
        {
            Do(Circle(500, 1000, 3000, false, false));
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