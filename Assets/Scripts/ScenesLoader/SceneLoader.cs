using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.ScenesLoader
{
    public class SceneLoader
    {
        public void MainMenu()
        {
            SceneManager.LoadSceneAsync(0);
        }
        public void Game()
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
}
