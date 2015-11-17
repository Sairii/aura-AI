//--- Aura Script -----------------------------------------------------------
//  Sheep AI
//--- Description -----------------------------------------------------------
//  AI for sheeps.
//---------------------------------------------------------------------------

[AiScript("sheep")]
public class SheepAi : AiScript
{
    public SheepAi()
    {
        // visual range 400 - 90°, audio range 300
        SetAggroRadius(400);

        Hates("/wolf/");
    }

    protected override IEnumerable Idle()
    {
        Do(Wander(100, 400));
        Do(Wait(10000, 14000));
    }

    protected override IEnumerable Alert()
    {
        var rndAlert = Random();
        if (rndAlert < 25)
        {
            Do(PrepareSkill(SkillId.Defense));
        }
        Do(Wait(2000, 4000));
        Do(CancelSkill());
        Do(KeepDistance(800, false));

    }

    protected override IEnumerable Aggro()
    {
        Do(Attack(3));
        Do(Wait(3000, 8000));
    }
}
