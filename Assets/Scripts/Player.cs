using System;
using UnityEngine;

public class Player : MonoBehaviour {
    
    public static Player Instance { get; private set; }

    public event EventHandler OnCoinPickup;
    public event EventHandler<OnLevelEndEventArgs> OnLevelEnd;

    public class OnLevelEndEventArgs : EventArgs {
        public EndType endType;
        public int score;
    }

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask slipperyGroundLayerMask;

    private int score;
    private Rigidbody2D playerRigidbody2D;
    private CapsuleCollider2D playerCapsuleCollider2D;
    private bool doubleJump;
    private bool isJumpActionPressed;
    private bool isOnSlipperySurface;

    public enum EndType {
        LevelComplete,
        LevelFail,
    }
    
    private void Start() {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void Awake() {
        Instance = this;
        playerCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }
    

    private void Update() {
        if (GameInput.Instance.IsJumpActionPressed()) {
            isJumpActionPressed = true;
        }
        isOnSlipperySurface = playerRigidbody2D.IsTouchingLayers(slipperyGroundLayerMask);
    }
    
    private void FixedUpdate() {
        
        if (GameInput.Instance.IsRightActionPressed() && MathF.Abs(playerRigidbody2D.linearVelocityX) < maxMoveSpeed) {
            playerRigidbody2D.AddForce(transform.right * moveSpeed);
        } else if (!GameInput.Instance.IsRightActionPressed() && !GameInput.Instance.IsLeftActionPressed()) {
            if (IsGrounded() && !isOnSlipperySurface) {
                playerRigidbody2D.linearVelocityX = 0;
            }
        } else if (GameInput.Instance.IsRightActionPressed() && GameInput.Instance.IsLeftActionPressed()) {
            playerRigidbody2D.linearVelocityX = 0;
        }
        if (GameInput.Instance.IsLeftActionPressed() && MathF.Abs(playerRigidbody2D.linearVelocityX) < maxMoveSpeed) {
            playerRigidbody2D.AddForce(-transform.right * moveSpeed);
        } else if (!GameInput.Instance.IsRightActionPressed() && !GameInput.Instance.IsLeftActionPressed()) {
            if (IsGrounded() && !isOnSlipperySurface) {
                playerRigidbody2D.linearVelocityX = 0;
            }
        } else if (GameInput.Instance.IsRightActionPressed() && GameInput.Instance.IsLeftActionPressed()) {
            playerRigidbody2D.linearVelocityX = 0;
        }

        if (IsGrounded() && !isJumpActionPressed) {
            doubleJump = false;
        }
        
        if (isJumpActionPressed) {
            isJumpActionPressed = false;
            if (IsGrounded() || doubleJump) {
                playerRigidbody2D.linearVelocityY = jumpForce;
                
                doubleJump = !doubleJump;
            }
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.TryGetComponent(out DeadlyObject deadlyObject)) {
            OnLevelEnd?.Invoke(this, new OnLevelEndEventArgs {
                endType = EndType.LevelFail,
                score = score,
            });
            Destroy(gameObject);
            GameInput.Instance.Dispose();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out CoinPickup coinPickup)) {
            OnCoinPickup?.Invoke(this, EventArgs.Empty);
            coinPickup.DestroySelf();
        }

        if (collision.gameObject.TryGetComponent(out EndDoor endDoor)) {
            OnLevelEnd?.Invoke(this, new OnLevelEndEventArgs {
                endType = EndType.LevelComplete,
                score = score,
            });
            Destroy(gameObject);
            GameInput.Instance.Dispose();
        }
    }

    private bool IsGrounded() {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(playerCapsuleCollider2D.bounds.center,
            playerCapsuleCollider2D.bounds.size, playerCapsuleCollider2D.direction, 0f, Vector2.down, extraHeight, groundLayerMask | slipperyGroundLayerMask);
        return raycastHit.collider != null;
    }
}