using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-999)]
public class GameManager : MonoBehaviour
{
    #region ranges of movement
    [SerializeField] private Vector2 tutorialZoneUpRange;
    [SerializeField] private Vector2 outerZoneUpRange;
    [SerializeField] private Vector2 firstTentZoneUpRange;
    [SerializeField] private Vector2 secondTentZoneUpRange;
    
    [SerializeField] private Vector2 tutorialZoneDownRange;
    [SerializeField] private Vector2 outerZoneDownRange;
    [SerializeField] private Vector2 firstTentZoneDownRange;
    [SerializeField] private Vector2 secondTentZoneDownRange;
    [SerializeField] private Vector2 prelastZoneDownRange;
    [SerializeField] private Vector2 lastZoneDownRange;
    #endregion
    
    [SerializeField] private ClownMovement[] clownMovements;
    private static GameManager _gameManager;
    private bool _inAction;
    private bool _isPopup;
    private Dictionary<Locations, Vector2> _accessibleLocationsRangeDict;
    private Dictionary<Direction, ClownMovement> _playersMovementDict;
    private Direction currentZone = Direction.UP;
    
    public Camera mainCamera;
    public GameObject InventoryContentContext;

    public enum Direction
    {
        UP, DOWN
    }
    
    public enum Locations
    {
        TutorialZoneUp, TutorialZoneDown, OuterZoneUp, OuterZoneDown, FirstTentZoneUp, FirstTentZoneDown,
        SecondTentZoneUp, SecondTentZoneDown,PreLastZoneDown, LastZoneDown
    }
    
    private void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = this;
        }
        _inAction = false;
        _accessibleLocationsRangeDict = new Dictionary<Locations, Vector2>();
        _playersMovementDict = new Dictionary<Direction, ClownMovement>();
    }

    private void Start()
    {
        _playersMovementDict[Direction.UP] = clownMovements[0];
        _playersMovementDict[Direction.DOWN] = clownMovements[1];
        _accessibleLocationsRangeDict[Locations.TutorialZoneUp] = tutorialZoneUpRange;
        _accessibleLocationsRangeDict[Locations.TutorialZoneDown] = tutorialZoneDownRange;
        _accessibleLocationsRangeDict[Locations.OuterZoneUp] = outerZoneUpRange;
        _accessibleLocationsRangeDict[Locations.OuterZoneDown] = outerZoneDownRange;
        _accessibleLocationsRangeDict[Locations.FirstTentZoneUp] = firstTentZoneUpRange;
        _accessibleLocationsRangeDict[Locations.FirstTentZoneDown] = firstTentZoneDownRange;
        _accessibleLocationsRangeDict[Locations.SecondTentZoneUp] = secondTentZoneUpRange;
        _accessibleLocationsRangeDict[Locations.SecondTentZoneDown] = secondTentZoneDownRange;
        _accessibleLocationsRangeDict[Locations.LastZoneDown] = lastZoneDownRange;
        _accessibleLocationsRangeDict[Locations.PreLastZoneDown] = prelastZoneDownRange;
    }

    public static GameManager Shared()
    {
        return _gameManager;
    }

    public void SetIsActionActive(bool isActive)
    {
        _inAction = isActive;
    }

    public bool GetActionActive()
    {
        return _inAction;
    }
    
    public void SetIsPopup(bool isPopup)
    {
        _isPopup = isPopup;

        if (_isPopup)
        {
            Cursor.Shared().OnOpenPopup();
        }
        else
        {
            Cursor.Shared().OnClosePopup();
        }
    }

    public bool GetIsPopup()
    {
        return _isPopup;
    }

    public void SetCurrentZone(Direction zone)
    {
        currentZone = zone;
    }
    
    public Direction GetCurrentZone()
    {
        return currentZone;
    }

    public void AddRangeToLocation(Locations location, Vector2 range) //todo: check if all of this should be on floor
    {
        if (!_accessibleLocationsRangeDict.ContainsKey(location)) //if the range didn't set yet
        {
            _accessibleLocationsRangeDict[location] = range;
            return;
        }
        if (_accessibleLocationsRangeDict[location][1] >= range[1]) // if the end of the new range is smaller then before
            return;
        if (range[0] <= _accessibleLocationsRangeDict[location][1]) //if the range start inside other range then merge them
        {
            _accessibleLocationsRangeDict[location] = new Vector2(_accessibleLocationsRangeDict[location].x, range.y);
        }
    }

    public Vector2 GetRangeOfLocation(Locations location)
    {
        return _accessibleLocationsRangeDict[location];
    }

    public void InActionForSeconds(float seconds)
    {
        StartCoroutine(InActionForSecondsCoroutine(seconds));
    }
    
    private IEnumerator InActionForSecondsCoroutine(float seconds)
    {
        _inAction = true;
        yield return new WaitForSeconds(seconds);
        _inAction = false;
    }

    public ClownMovement GetClownMovement(Direction direction)
    {
        return _playersMovementDict[direction];
    }

    public IEnumerator ProFadeAnimation(SpriteRenderer beforeSpriteRenderer, SpriteRenderer afterSpriteRenderer, float speed, Action funcToDo, GameObject objDisable)
    {
        Color _transparent = Color.white;
        _transparent.a = 0;
        Color _itemColor = beforeSpriteRenderer.color;
        float progress = 0f;
        while (progress < 1)
        {
            beforeSpriteRenderer.color = Color.Lerp(_itemColor, _transparent, progress);
            afterSpriteRenderer.color = Color.Lerp(_transparent, _itemColor, progress);
            progress += Time.deltaTime * speed;
            yield return null;
        }
        beforeSpriteRenderer.color = Color.clear;
        afterSpriteRenderer.color = _itemColor;
        funcToDo.Invoke();
        objDisable.SetActive(false);
    }
    
    public IEnumerator ProFadeWithItemAnimation(SpriteRenderer beforeSpriteRenderer, SpriteRenderer afterSpriteRenderer, float speed, Action funcToDo, GameObject objDisable, GameObject objectToEnable, SpriteRenderer objectToEnableSprite)
    {
        Color _transparent = Color.white;
        _transparent.a = 0;
        Color _itemColor = beforeSpriteRenderer.color;
        float progress = 0f;
        objectToEnableSprite.color = _transparent;
        objectToEnable.SetActive(true);
        
        while (progress < 1)
        {
            beforeSpriteRenderer.color = Color.Lerp(_itemColor, _transparent, progress);
            afterSpriteRenderer.color = Color.Lerp(_transparent, _itemColor, progress);
            objectToEnableSprite.color = afterSpriteRenderer.color;
            progress += Time.deltaTime * speed;
            yield return null;
        }
        
        beforeSpriteRenderer.color = Color.clear;
        afterSpriteRenderer.color = _itemColor;
        funcToDo.Invoke();
        objDisable.SetActive(false);
    }
    
    public IEnumerator MultiProFadeWithItemAnimation(SpriteRenderer[] beforeSpriteRenderers, SpriteRenderer[] afterSpriteRenderers, float speed, Action funcToDo, GameObject objDisable, GameObject objectToEnable, SpriteRenderer objectToEnableSprite)
    {
        Color _transparent = Color.white;
        _transparent.a = 0;
        Color _itemColor = beforeSpriteRenderers[0].color;
        float progress = 0f;
        objectToEnableSprite.color = _transparent;
        objectToEnable.SetActive(true);
        
        while (progress < 1)
        {
            foreach (var before in beforeSpriteRenderers)
            {
                before.color = Color.Lerp(_itemColor, _transparent, progress);
            }
            
            foreach (var after in afterSpriteRenderers)
            {
                after.color = Color.Lerp(_transparent, _itemColor, progress);
            }
            
            objectToEnableSprite.color = afterSpriteRenderers[0].color;
            progress += Time.deltaTime * speed;
            yield return null;
        }
        
        foreach (var before in beforeSpriteRenderers)
        {
            before.color = Color.clear;
        }
            
        foreach (var after in afterSpriteRenderers)
        {
            after.color = _itemColor;
        }
        
        funcToDo.Invoke();
        objDisable.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
