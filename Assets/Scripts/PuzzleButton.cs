using UnityEngine;

public class PuzzleButton : MonoBehaviour {

    [SerializeField] private PuzzleDoor[] puzzleDoors;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private Vector3 buttonMovementOnPress = new Vector3(0f, 0.3f, 0f);
    

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            ButtonPressed();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            ButtonReleased();
        }
    }

    private void ButtonPressed() {
        spriteRenderer.transform.Translate(-buttonMovementOnPress);
        foreach (PuzzleDoor puzzleDoor in puzzleDoors) {
            puzzleDoor.ToggleDoorAppear();
        }
    }

    private void ButtonReleased() {
        spriteRenderer.transform.Translate(buttonMovementOnPress);
    }
}
