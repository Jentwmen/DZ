using UnityEngine;

public class Ground : MonoBehaviour
    {
        public bool OnGround { get; private set; } //находится на земле или нет
        public float Friction { get; private set; } //для трения

        private Vector2 _normal;
        private PhysicsMaterial2D _material;

        private void OnCollisionExit2D(Collision2D collision) //встроенный метод, срабатывает когда два коллайдера перестают взаимодействовать
        {
            OnGround = false;
            Friction = 0;
        }

        private void OnCollisionEnter2D(Collision2D collision) //встроенный метод, срабатывает когда два коллайдера соприкасаются
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }

        private void EvaluateCollision(Collision2D collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                _normal = collision.GetContact(i).normal;  //проверка по всем точкам, если наклонная поверхность, то игрок не стоит
                OnGround |= _normal.y >= 0.6f;
            }
        }

        private void RetrieveFriction(Collision2D collision)  //проверка материала и получение трения на материале
        {
            _material = collision.rigidbody.sharedMaterial;

            Friction = 0;

            if(_material != null)
            {
                Friction = _material.friction;
            }
        }
    }