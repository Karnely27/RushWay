using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TableSelect : MonoBehaviour
{
    [SerializeField] private EmptySelect _emptySelect;
    [SerializeField] private GameObject _contentSelectMenu;
    [SerializeField] private GameObject _contentTableMenu;
    [SerializeField] private SelectView _selectView;
    [SerializeField] private Button _startGameButton;

    private CreatureViewSelect[] _creatures;

    private void OnEnable()
    {
        _creatures = _contentSelectMenu.GetComponentsInChildren<CreatureViewSelect>();
        _startGameButton.onClick.AddListener(OnButtonClick);

        foreach (var creature in _creatures)
        {
            creature.OnSelected += SetCreatureInTable;
            creature.OnDeleted += SetImage;
        }
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveListener(OnButtonClick);

        foreach (var creature in _creatures)
        {
            creature.OnSelected -= SetCreatureInTable;
            creature.OnDeleted -= SetImage;
        }
    }

    private void SetCreatureInTable(Creature creature)
    {
        var images = _contentTableMenu.GetComponentsInChildren<EmptySelect>();

        if(images.Length > 0)
        {
            images[0].SetCreature();
            var view = Instantiate(_selectView, _contentTableMenu.transform);
            view.Render(creature);
        }
    }

    private void SetImage(Creature creature)
    {
        var image = Instantiate(_emptySelect, _contentTableMenu.transform);
    }

    private void OnButtonClick()
    {
        TryStartGame();
    }

    private void TryStartGame()
    {
        var images = _contentTableMenu.GetComponentsInChildren<EmptySelect>();

        if (images.Length <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
