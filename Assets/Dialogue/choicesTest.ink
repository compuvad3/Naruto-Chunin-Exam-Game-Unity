VAR winningRate = 50
VAR shurikens = 25
VAR endReached = 0

.


-> Intro

=== Intro ===
Hey, Naruto! Welcome to the Chunin Exam. You are about to start the first phase of the exam.
In this phase you will have to answer multiple questions about the events that happened to you and your friends recently.
More correctly answered questions will result in more shurikens in your inventory. You will need them to successfully complete the second phase of the exam.
Are you ready to begin the first pahse of the Chunin Exam?
    *[Yes]
        Perfect!
        -> LandOfWaves1
    *[No]
        Come back when you are ready.
        -> END

=== LandOfWaves1 ===
Let's begin with the Land of Waves arc.
Who is the wealthy client who hires Team 7 for their mission in the Land of Waves?
    *[Tazuna]
        Correct!
        ~winningRate += 2
        ~shurikens += 2
        shurikens + 2
        -> LandOfWaves2
    *[Zabuza]
        Incorrect
        ~winningRate -= 6
        ~shurikens -= 6
        shurikens - 6
        -> ChuninExams1

=== LandOfWaves2 ===
What is the primary reason Zabuza and Haku target the Land of Waves?
    *[To steal valuable artifacts]
        Wrong
        ~winningRate -= 3
        ~shurikens -= 3
        shurikens - 3
        -> ChuninExams1
    *[To assassinate Tazuna]
        Right!
        ~winningRate += 2
        ~shurikens += 2
        shurikens + 2
        -> LandOfWaves3

=== LandOfWaves3 ===
What is the name of the massive body of water surrounding the Land of Waves?
    *[Sea of Fire]
        Correct
        ~winningRate += 3
        ~shurikens += 3
        shurikens + 3
        -> LandOfWaves4
    *[Sea of Trees]
        Incorrect!
        ~winningRate -= 3
        ~shurikens -= 3
        shurikens - 3
        -> ChuninExams1

=== LandOfWaves4 ===
What is the forbidden technique that Haku uses during his battle against Sasuke and Naruto?
    *[Shadow Clone Jutsu]
        Wrong
        ~winningRate -= 2
        ~shurikens -= 2
        shurikens - 2
        -> ChuninExams1
    *[Demonic Ice Mirrors]
        Right!
        ~winningRate += 3
        ~shurikens += 3
        shurikens + 3
        -> LandOfWaves5

=== LandOfWaves5 ===
Which member of Team 7 demonstrates the "Hidden Mist Jutsu" during the Land of Waves arc?
    *[Kakashi]
        Correct
        ~winningRate += 6
        ~shurikens += 6
        shurikens + 6
        -> ChuninExams1
    *[Sasuke]
        Incorrect!
        ~winningRate -= 2
        ~shurikens -= 2
        shurikens - 2
        -> ChuninExams1

=== ChuninExams1 ===
Now, we'll focus on Chunin Exam arc.
Where do the Chunin Exams take place?
    *[Konohagakure]
        Correct
        ~winningRate += 2
        ~shurikens += 2
        shurikens + 2
        -> ChuninExams2
    *[Sunagakure]
        Incorrect!
        ~winningRate -= 8
        ~shurikens -= 8
        shurikens - 8
        -> KonohaCrush1

=== ChuninExams2 ===
Who is Naruto's primary rival during the Chunin Exams?
    *[Sasuke]
        Correct
        ~winningRate += 2
        ~shurikens += 2
        shurikens + 2
        -> ChuninExams3
    *[Sakura]
        Incorrect!
        ~winningRate -= 4
        ~shurikens -= 4
        shurikens - 4
        -> KonohaCrush1

=== ChuninExams3 ===
Which character infiltrates the Chunin Exams in disguise to gather information about the participating ninja?
    *[Orochimaru]
        Correct
        ~winningRate += 4
        ~shurikens += 4
        shurikens + 4
        -> ChuninExams4
    *[Jiraiya]
        Incorrect!
        ~winningRate -= 4
        ~shurikens -= 4
        shurikens - 4
        -> KonohaCrush1

=== ChuninExams4 ===
What is the first phase of the Chunin Exams, designed to test a ninja's knowledge and information-gathering skills?
    *[Written Exam]
        Correct
        ~winningRate += 4
        ~shurikens += 4
        shurikens + 4
        -> ChuninExams5
    *[Combat Exam]
        Incorrect!
        ~winningRate -= 2
        ~shurikens -= 2
        shurikens - 2
        -> KonohaCrush1

=== ChuninExams5 ===
Who is the leader of Team 7 during the Chunin Exams?
    *[Kakashi]
        Correct
        ~winningRate += 8
        ~shurikens += 8
        shurikens + 8
        -> KonohaCrush1
    *[Iruka]
        Incorrect!
        ~winningRate -= 2
        ~shurikens -= 2
        shurikens - 2
        -> KonohaCrush1

=== KonohaCrush1 ===
Next ones are about the Konoha Crush arc.
Who is the leader of the invading force during the Konoha Crush arc?
    *[Orochimaru]
        Correct
        ~winningRate += 2
        ~shurikens += 2
        shurikens + 2
        -> KonohaCrush2
    *[Itachi Uchiha]
        Incorrect!
        ~winningRate -= 8
        ~shurikens -= 8
        shurikens - 8
        -> TsunadeSearch1

=== KonohaCrush2 ===
What is the primary goal of the attackers during the Konoha Crush?
    *[To capture the Nine-Tails, Naruto Uzumaki]
        Wrong
        ~winningRate -= 4
        ~shurikens -= 4
        shurikens - 4
        -> TsunadeSearch1
    *[To destroy the Hidden Leaf Village (Konohagakure)]
        Right!
        ~winningRate += 2
        ~shurikens += 2
        shurikens + 2
        -> KonohaCrush3

=== KonohaCrush3 ===
Who is the Third Hokage of Konohagakure during the Konoha Crush arc?
    *[Minato Namikaze]
        Wrong
        ~winningRate -= 4
        ~shurikens -= 4
        shurikens - 4
        -> TsunadeSearch1
    *[Hiruzen Sarutobi]
        Right!
        ~winningRate += 4
        ~shurikens += 4
        shurikens + 4
        -> KonohaCrush4

=== KonohaCrush4 ===
What technique does Orochimaru use to summon the First and Second Hokages during the Konoha Crush?
    *[Edo Tensei]
        Correct
        ~winningRate += 4
        ~shurikens += 4
        shurikens + 4
        -> KonohaCrush5
    *[Rasengan]
        Incorrect!
        ~winningRate -= 2
        ~shurikens -= 2
        shurikens - 2
        -> TsunadeSearch1

=== KonohaCrush5 ===
Which member of Team 7 is critically injured during the battle against Orochimaru and the Hokage's reanimated predecessors?
    *[Sasuke]
        Correct
        ~winningRate += 8
        ~shurikens += 8
        shurikens + 8
        -> TsunadeSearch1
    *[Naruto]
        Incorrect!
        ~winningRate -= 2
        ~shurikens -= 2
        shurikens - 2
        -> TsunadeSearch1

=== TsunadeSearch1 ===
The next questions will be on the Search of Tsunade arc.
Who is selected to lead the mission to search for Tsunade, the Fifth Hokage candidate?
    *[Jiraiya]
        Correct
        ~winningRate += 2
        ~shurikens += 2
        shurikens + 2
        -> TsunadeSearch2
    *[Kakashi]
        Incorrect!
        ~winningRate -= 8
        ~shurikens -= 8
        shurikens - 8
        -> SasukeRecovery1

=== TsunadeSearch2 ===
Who is Tsunade's trusted companion and bodyguard during her time away from Konohagakure?
    *[Jiraiya]
        Wrong
        ~winningRate -= 4
        ~shurikens -= 4
        shurikens - 4
        -> SasukeRecovery1
    *[Shizune]
        Right!
        ~winningRate += 2
        ~shurikens += 2
        shurikens + 2
        -> TsunadeSearch3

=== TsunadeSearch3 ===
What is Tsunade's special technique that allows her to store and release chakra for immense strength and regeneration?
    *[Byakugan]
        Wrong
        ~winningRate -= 4
        ~shurikens -= 4
        shurikens - 4
        -> SasukeRecovery1
    *[Strength of a Hundred Seal]
        Right!
        ~winningRate += 4
        ~shurikens += 4
        shurikens + 4
        -> TsunadeSearch4

=== TsunadeSearch4 ===
What is the name of Tsunade's younger brother who died during a mission, greatly impacting her life?
    *[Nawaki]
        Correct
        ~winningRate += 4
        ~shurikens += 4
        shurikens + 4
        -> TsunadeSearch5
    *[Danzo]
        Incorrect!
        ~winningRate -= 2
        ~shurikens -= 2
        shurikens - 2
        -> SasukeRecovery1

=== TsunadeSearch5 ===
Who is the Fourth Hokage, whose likeness Naruto uses to inspire Tsunade to take up the role of Hokage?
    *[Minato Namikaze]
        Correct
        ~winningRate += 8
        ~shurikens += 8
        shurikens + 8
        -> SasukeRecovery1
    *[Hiruzen Sarutobi]
        Incorrect!
        ~winningRate -= 2
        ~shurikens -= 2
        shurikens - 2
        -> SasukeRecovery1

=== SasukeRecovery1 ===
Finally, the last arc - Sasuke Recovery Mission.
What is the primary objective of the Sasuke Retrieval Arc?
    *[To bring Sasuke back to Konohagakure]
        Correct
        ~winningRate += 2
        ~shurikens += 2
        shurikens + 2
        -> SasukeRecovery2
    *[To eliminate Sasuke to prevent his betrayal]
        Incorrect!
        ~winningRate -= 6
        ~shurikens -= 6
        shurikens - 6
        -> Results

=== SasukeRecovery2 ===
Who is the leader of the team formed to retrieve Sasuke?
    *[Naruto]
        Wrong
        ~winningRate -= 6
        ~shurikens -= 6
        shurikens - 6
        -> Results
    *[Shikamaru]
        Right!
        ~winningRate += 2
        ~shurikens += 2
        shurikens + 2
        -> SasukeRecovery3

=== SasukeRecovery3 ===
Which member of Team 7 is severely injured and left in a coma during the mission to retrieve Sasuke?
    *[Naruto]
        Correct
        ~winningRate += 4
        ~shurikens += 4
        shurikens + 4
        -> SasukeRecovery4
    *[Sakura]
        Incorrect!
        ~winningRate -= 4
        ~shurikens -= 4
        shurikens - 4
        -> Results

=== SasukeRecovery4 ===
During the Sasuke Recovery Mission, which village provides assistance to the group from Konohagakure?
    *[Sunagakure]
        Correct
        ~winningRate += 4
        ~shurikens += 4
        shurikens + 4
        -> SasukeRecovery5
    *[Otokagure]
        Incorrect!
        ~winningRate -= 4
        ~shurikens -= 4
        shurikens - 4
        -> Results

=== SasukeRecovery5 ===
Who engages in a fierce one-on-one battle with Sasuke in the final showdown of the Sasuke Retrieval Arc?
    *[Naruto]
        Correct
        ~winningRate += 6
        ~shurikens += 6
        shurikens + 6
        -> SasukeRecovery6
    *[Gaara]
        Incorrect!
        ~winningRate -= 2
        ~shurikens -= 2
        shurikens - 2
        -> Results
        
=== SasukeRecovery6 ===
Which member of Team 7 initially leaves Konohagakure to search for Sasuke alone before the mission is organized?
    *[Sakura]
        Correct
        ~winningRate += 6
        ~shurikens += 6
        shurikens + 6
        -> Results
    *[Naruto]
        Incorrect!
        ~winningRate -= 2
        ~shurikens -= 2
        shurikens - 2
        -> Results

=== Results ===
{shurikens < 0 :
    ~shurikens = 0
}
{shurikens > 100 :
    ~shurikens = 100
}

{winningRate < 0 :
    ~winningRate = 0
}

{winningRate > 100 :
    ~winningRate = 100
}

Nice job!
Here is your result of the first phase of the Chunin Exam:
Winning rate = {winningRate}
Total number of shurikens = {shurikens}
The next phase will be a combat exam, where you will be allowed to use only shurikens. You will have several enemies to encounter.
Good luck!
~endReached = 1     // reached the end of the dialogue, can transition to the next stage
-> END

