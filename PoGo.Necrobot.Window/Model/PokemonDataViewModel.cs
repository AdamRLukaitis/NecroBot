﻿using PoGo.NecroBot.Logic.PoGoUtils;
using POGOProtos.Data;
using POGOProtos.Enums;
using POGOProtos.Inventory;
using POGOProtos.Settings.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoGo.Necrobot.Window.Model
{
    public class PokemonDataViewModel : ViewModelBase
    {
        public PokemonDataViewModel(PokemonData pokemon, PokemonSettings setting, Candy candy)
        {
            this.PowerupText = "Upgrade";
            this.AllowPowerup = true;

            this.PokemonData = pokemon;
            this.Id = pokemon.Id;
            this.PokemonName = pokemon.PokemonId;
            this.HP = pokemon.Stamina;
            this.MaxHP = pokemon.StaminaMax;
            this.IV = PokemonInfo.CalculatePokemonPerfection(pokemon);
            this.CP = PokemonInfo.CalculateCp(pokemon);
            this.Level = (int)PokemonInfo.GetLevel(pokemon);
            this.Favorited = pokemon.Favorite > 0;
            this.IsSelected = false;
            this.Move1 = pokemon.Move1.ToString().Replace("Fast", "");
            this.Move2 = pokemon.Move2.ToString();
            
            this.PokemonSettings = setting;
            this.AllowEvolve = candy.Candy_ >= setting.CandyToEvolve && setting.EvolutionIds.Count > 0;
            this.Candy = candy.Candy_;
        }

        internal void UpdateWith(PokemonData item, Candy candy = null)
        {
            this.IsTransfering = false;
            this.IsEvolving = false;
            this.IsFavoriting = false;
            this.CP = item.Cp;
            this.Level = (int)PokemonInfo.GetLevel(item);
            if (candy != null)
            {
                this.Candy = candy.Candy_;
                this.AllowEvolve = candy.Candy_ >= this.PokemonSettings.CandyToEvolve && this.PokemonSettings.EvolutionIds.Count > 0;
            }
        }

        private bool isTransfering;
        public bool IsTransfering
        {
            get { return isTransfering; }
            set
            {
                isTransfering = value;
                RaisePropertyChanged("IsTransfering");

            }
        }
        public string Move1 { get; set; }
        public string Move2 { get; set; }

        private bool isEvolving;
        public bool IsEvolving
        {
            get { return isEvolving; }
            set
            {
                isEvolving = value;
                RaisePropertyChanged("IsEvolving");

            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        public PokemonId PokemonName { get; set; }

        public int Level { get; set; }

        private int candy;
        public int Candy
        {
            get { return candy; }
            set
            {
                this.candy = value;
                RaisePropertyChanged("Candy");
            }
        }
        private int cp;
        public int CP
        {
            get { return cp; }
            set
            {
                this.cp = value;
                RaisePropertyChanged("CP");
            }
        }

        public double IV { get; set; }

        public DateTime CaughtTime { get; set; }

        public ulong Id { get; set; }

        private int hp;
        public int HP
        {
            get { return hp; }
            set
            {
                hp = value;
                RaisePropertyChanged("HP");
                RaisePropertyChanged("HPDisplay");
            }
        }
        
        private int maxHp;
        public int MaxHP
        {
            get { return maxHp; }
            set
            {
                maxHp = value;
                RaisePropertyChanged("MaxHP");
                RaisePropertyChanged("HPDisplay");
            }
        }

        private bool favorited;

        public bool Favorited
        {
            get { return favorited; }
            set
            {
                favorited = value;
                RaisePropertyChanged("Favorited");
            }
        }
        public string HPDisplay => $"{HP}/{MaxHP}";

        private bool allowEvolve;

        public bool AllowEvolve
        {
            get { return allowEvolve; }
            set
            {
                this.allowEvolve = value;
                RaisePropertyChanged("AllowEvolve");
            }
        }

        public PokemonSettings PokemonSettings { get; private set; }
        public bool IsFavoriting { get; set; }

        public string PokemonIcon
        {
            get
            {
                if ((int)PokemonData.PokemonId > 151)
                {

                    return $"https://rankedboost.com/wp-content/plugins/ice/riot/poksimages/pokemons2/{(int)PokemonData.PokemonId:000}.png";

                }

                return $"https://rankedboost.com/wp-content/plugins/ice/riot/poksimages/pokemons/{(int)PokemonData.PokemonId:000}.png";
            }
        }

        public PokemonData PokemonData { get; set; }
        public bool AllowPowerup { get; internal set; }
        public string PowerupText { get; internal set; }
    }
}
