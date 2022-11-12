using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IronTrauma {
    public class Sonar : MonoBehaviour {
        public float SonarRadius;
        public float VisualSphereRadius;
        
        public Transform SubmarineWaveSource;

        public List<Transform> MiniCubes;

        public float Zoom = 1;
        
        public float ObjectSizeMultiplier;
        
        public float SonarScale => VisualSphereRadius * Zoom / SonarRadius;

        void Update() {
            var hits = DetectObjects();
            if ( hits.Length > MiniCubes.Count ) {
                Debug.LogWarning($"counted objects more than can show on sonar. Showing only first {MiniCubes.Count}");
            }
            for ( var i = 0; i < MiniCubes.Count; i++ ) {
                var hit  = i < hits.Length ? hits[i] : null;
                var cube = MiniCubes[i];
                InitCube(cube, hit);
            }
        }

        Collider[] DetectObjects() {
            // use only HitBox Layer;
            var usedLayer = 1 << 9;
            return Physics.OverlapSphere(SubmarineWaveSource.position, SonarRadius, usedLayer);
        }

        void InitCube(Transform miniCube, Collider hitObject) {
            if ( !hitObject ) {
                miniCube.gameObject.SetActive(false);
                return;
            }
            miniCube.gameObject.SetActive(true);
            var realObjectTransform = hitObject.gameObject.GetComponent<Transform>();
            var minimapPosition     = (realObjectTransform.position - SubmarineWaveSource.position) * SonarScale + transform.position;
            miniCube.position   = minimapPosition;
            var aabb = hitObject.bounds;
            miniCube.localScale                      = aabb.size * SonarScale * ObjectSizeMultiplier;
            if ( hitObject.gameObject.TryGetComponent<MeshFilter>(out var meshFilter) ) {
                miniCube.GetComponent<MeshFilter>().mesh = meshFilter.mesh;
            }
        }
        
        void OnDrawGizmosSelected() {
            Gizmos.DrawWireSphere(SubmarineWaveSource.position, SonarRadius);
            Gizmos.DrawWireSphere(transform.position, VisualSphereRadius);
        }
    }
}
