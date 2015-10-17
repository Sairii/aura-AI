//--- Aura Script -----------------------------------------------------------
//  Wisp AI
//--- Description -----------------------------------------------------------
//  AI for wisps.
//---   Missing   -----------------------------------------------------------
//  Stacking Magic Attack and the usual
//---------------------------------------------------------------------------

[AiScript("wisp")]
public class WispAi : AiScript
{
    string[] WispChat = new[] { "?", "!?!?", "???" };

    public WispAi()
    {
        SetAggroRadius(950); // Angle 120 Audio 400
        SetAggroDelay(7000);
        SetAggroLimit(AggroLimit.None);
        Doubts("/pc/", "/pet/");

        On(AiState.Aggro, AiEvent.DefenseHit, OnDefenseHit);
        On(AiState.Aggro, AiEvent.Hit, OnHit);
        On(AiState.Aggro, AiEvent.KnockDown, OnKnockDown);
    }

    protected override IEnumerable Idle()
    {
        var rndidle = Random();
        if (rndidle < 10)
        {
            Do(Wander(100, 500));
        }
        else if (rndidle < 40)
        {
            Do(Wander(100, 500, false));
        }
        else if (rndidle < 60)
        {
            Do(Wait(4000, 6000));
        }
        else if (rndidle < 70)
        {
            Do(Say("!!!")); // says this before charging
            Do(PrepareSkill(SkillId.Lightningbolt));
        }
        Do(Wait(2000, 5000));
    }

    protected override IEnumerable Alert()
    {
        Do(CancelSkill());
        var rndalert = Random();
        if (rndalert < 20) // 20%
        {
            Do(Say(WispChat[Random(WispChat.Length)]));
            Do(Wait(1000, 2000));
        }
        else if (rndalert < 50) // 30%
        {
            Do(Say(WispChat[Random(WispChat.Length)]));
            Do(Wait(1000, 4000));
            if (Random() < 50)
            {
                Do(Say(WispChat[Random(WispChat.Length)]));
                Do(Circle(600, 1000, 2000, true));
            }
            else
            {
                Do(Say(WispChat[Random(WispChat.Length)]));
                Do(Circle(600, 1000, 2000, false));
            }
        }
        Do(Say("!!!")); // says this before charging
        Do(PrepareSkill(SkillId.Lightningbolt));
        Do(Wait(2000, 10000));
    }

    protected override IEnumerable Aggro()
    {
        var rndagr = Random();
        if (rndagr < 10) // 10%
        {
            if (Random() < 50)
            {
                Do(Wander(100, 200));
            }
            Do(Say("!"));
            Do(Attack(3, 4000));
            var rndagr2 = Random();
            if (rndagr2 < 60) // 60%
            {
                Do(Say("!!!")); // says this before charging
                Do(PrepareSkill(SkillId.Lightningbolt));
            }
            else if (rndagr2 < 80) // 20%
            {
                Do(PrepareSkill(SkillId.Defense));
                Do(Say("!!"));
                Do(Follow(50, true, 1000));
            }
            Do(Wait(500, 2000));
        }
        else if (rndagr < 30) // 20%
        {
            Do(Say("!!!")); // says this before charging
            var charge = Random();
            if (charge < 56)
            {
                Do(PrepareSkill(SkillId.Lightningbolt)); // 1 charge
            }
            else if (charge < 67)
            {
                Do(PrepareSkill(SkillId.Lightningbolt)); // 2 charges
            }
            else if (charge < 78)
            {
                Do(PrepareSkill(SkillId.Lightningbolt)); // 3 charges
            }
            else if (charge < 89)
            {
                Do(PrepareSkill(SkillId.Lightningbolt)); // 4 charges
            }
            else
            {
                Do(PrepareSkill(SkillId.Lightningbolt)); // 5 charges
            }
            if (Random() < 80)
            {
                Do(Say("!"));
                Do(Attack(3, 4000));
            }
            Do(Wait(500, 2000));
        }
        else if (rndagr < 50) // 20%
        {
            var rndagr3 = Random();
            if (rndagr3 < 40)
            {
                Do(PrepareSkill(SkillId.Smash));
                Do(Say("!!!!"));
                Do(Attack(1, 4000));
            }
            else if (rndagr3 < 70)
            {
                Do(PrepareSkill(SkillId.Smash));
                Do(Say("!"));
                Do(CancelSkill());
                Do(Attack(3, 4000));
            }
            else
            {
                Do(PrepareSkill(SkillId.Defense));
                Do(Say("!!"));
                Do(Wait(2000, 7000));
            }
            Do(Wait(1000, 2000));
        }
        else if (rndagr < 60) // 10%
        {
            Do(PrepareSkill(SkillId.Defense));
            Do(Say("!!"));
            var rndagr4 = Random();
            if (rndagr4 < 30)
            {
                Do(Circle(400, 1000, 2000, true));
            }
            else if (rndagr4 < 30)
            {
                Do(Circle(400, 1000, 2000, false));
            }
            else
            {
                Do(Follow(400, true, 5000));
            }
            Do(CancelSkill());
        }
        else if (rndagr < 70) // 10%
        {
            var rndagr5 = Random();
            if (rndagr5 < 30)
            {
                Do(Circle(400, 1000, 2000, true, false));
            }
            else if (rndagr5 < 60)
            {
                Do(Circle(400, 1000, 2000, false, false));
            }
            else if (rndagr5 < 80)
            {
                Do(Follow(400, false, 5000));
            }
            else
            {
                Do(Timeout(5000, KeepDistance(1000, false)));
            }
        }
        else if (rndagr < 80) // 10%
        {
            Do(PrepareSkill(SkillId.Counterattack));
            Do(Say("!!!!!"));
            Do(Wait(1000, 10000));
            Do(CancelSkill());
        }
    }

    private IEnumerable OnDefenseHit()
    {
        Do(Say(WispChat[Random(WispChat.Length)]));
        Do(Attack(3, 4000));
        if (Random() < 40)
        {
            Do(Say("!!!")); // says this before charging
            Do(PrepareSkill(SkillId.Lightningbolt)); // 1 charge
            Do(Wait(1000, 2000));
        }
    }

    private IEnumerable OnHit()
    {
        if (Random() < 50)
        {
            Do(Timeout(2000, KeepDistance(1000, false)));
        }
        else
        {
            Do(Attack(3, 4000));
        }
    }

    private IEnumerable OnKnockDown()
    {
        var knockd = Random();
        if (knockd < 25)
        {
            Do(PrepareSkill(SkillId.Smash));
            Do(Say("!!!!"));
            Do(Attack(1, 4000));
        }
        else if (knockd < 75)
        {
            Do(PrepareSkill(SkillId.Defense));
            Do(Say("!!"));
            var knockd2 = Random();
            if (knockd2 < 30)
            {
                Do(Circle(400, 1000, 2000, true));
            }
            else if (knockd2 < 60)
            {
                Do(Circle(400, 1000, 2000, false));
            }
            else
            {
                Do(Follow(400, true, 5000));
            }
            Do(CancelSkill());
        }
        else
        {
            Do(Say("!"));
            Do(Attack(3, 4000));
            if (Random() < 40)
            {
                Do(Say("!!!")); // says this before charging
                Do(PrepareSkill(SkillId.Lightningbolt)); // 1 charge
                Do(Wait(1000, 2000));
            }
        }
    }
}