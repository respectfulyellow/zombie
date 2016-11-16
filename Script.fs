namespace Tests
// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

open Zombie
open NUnit.Framework
open FsUnit 

module Zombie =
    type Weapon = 
        | BaseballBat
        | FryingPan
        | Katana
        | Pistol
        | BottledWater
        | Molotov

    type Survivor = {
        Name: string
        Wounds: int
        RemainingActions: int
        InHand: Option<Weapon> array
        InReserve: Option<Weapon> array
    } 
    
    type Game = {Survivors:list<Survivor>;}
    
    let createGame = {Survivors = List.Empty}

    let addSurvivor game survivor = 
        { game with Survivors = survivor :: game.Survivors}

    let survive survivor = survivor.Wounds < 2

    let hit survivor = { survivor with InReserve = Array.sub survivor.InReserve 0 2 }

    let createNewSurvivor name = { 
        Name = name
        Wounds = 0
        RemainingActions = 3
        InHand = Array.init 2 (fun i -> None)
        InReserve = Array.init 3 (fun i -> None)
     }


// Define your library scripting code here
module ``Zombie Tests`` =
    open Zombie
    [<TestFixture>]
    type XXX() =
        [<Test>] member x.``survive should return true When less than 2 wounds``() =
            let colin = createNewSurvivor "Colin" in
            survive colin |> should equal true

        [<Test>] member x.
            ``Should start with 3 remaining actions``()=
                let eric = 
                    createNewSurvivor "Eric" in
                eric.RemainingActions |> should equal 3
        [<Test>] member x.
            ``Should remove weapon when hit``()=
                let ed = createNewSurvivor "Ed" in
                let ed' = hit ed in
                ed'.InReserve.Length|> should equal 2
        [<Test>] member x.
            ``add survivor``()=
                let game = createGame in
                let survivor = createNewSurvivor "Vlad" in
                let game' = addSurvivor game survivor in
                game'.Survivors.Head.Name |> should equal "Vlad"