using UnityEngine;

namespace SO
{

    [CreateAssetMenu(fileName = "AgentManagerChannelSO", menuName = "CypherAbility/AgentManagerChannelSO", order = 0)]
    public class AgentManagerChannelSO : ScriptableObject
    {
        public delegate void HighlightEnemies(float highlightTime);
        public event HighlightEnemies E_HighlightEnemies;

        public void RaiseHighlightEnemies(float highlightTime)
        {
            if (E_HighlightEnemies != null) E_HighlightEnemies.Invoke(highlightTime);
            else
            {
                Debug.LogWarning("No one is available to run HighlightEnemies");
            }
        }

        public delegate void CancelHighlightEnemies();
        public event CancelHighlightEnemies E_CancelHighlightEnemies;

        public void RaiseCancelHighlightEnemies()
        {
            if (E_CancelHighlightEnemies != null) E_CancelHighlightEnemies.Invoke();
            else
            {
                Debug.LogWarning("No one is available to run CancelHighlightEnemies");
            }
        }
    }

}