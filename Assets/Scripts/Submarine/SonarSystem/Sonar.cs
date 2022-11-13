using System.Collections.Generic;
using UnityEngine;

namespace IronTrauma.Submarine.SonarSystem {
    public class Sonar : MonoBehaviour {
        public SonarTerrainView TerrainView;
        
        public float SonarRadius;
        public float VisualSphereRadius;
        
        public Transform SubmarineWaveSource;
        
        public float Zoom = 1;
        
        public float ObjectSizeMultiplier;
        
        [Header("objects in sonar")]
        public Mesh FallbackMesh;
        public List<Transform> MiniCubes;
        public Material MiniaturesCommonMaterial;
        
        public float SonarScale => VisualSphereRadius * Zoom / SonarRadius;
        
        void Start() {
            TerrainView.Init();
        }
        
        void Update() {
            var hits = DetectObjects();
            if ( hits.Length > MiniCubes.Count ) {
                Debug.LogWarning($"counted objects more than can show on sonar. Showing only first {MiniCubes.Count}");
            }
            for ( var i = 0; i < MiniCubes.Count; i++ ) {
                var hit  = i < hits.Length ? hits[i] : null;
                var cube = MiniCubes[i];
                InitMiniature(cube, hit);
            }
            TerrainView.OnSonarUpdate(SonarScale, SubmarineWaveSource.position, transform.position);
            UpdateObjectsMaterial();
        }

        void DrawSonar() {
            
        }

        void UpdateObjectsMaterial() {
            MiniaturesCommonMaterial.SetVector("_Center", transform.position);
            MiniaturesCommonMaterial.SetFloat("_Distance", VisualSphereRadius);
        }

        Collider[] DetectObjects() {
            // use only HitBox Layer;
            var usedLayer = 1 << 9;
            return Physics.OverlapSphere(SubmarineWaveSource.position, SonarRadius, usedLayer);
        }

        void InitMiniature(Transform miniCube, Collider hitObject) {
            if ( !hitObject ) {
                miniCube.gameObject.SetActive(false);
                return;
            }
            miniCube.gameObject.SetActive(true);
            var realObjectTransform = hitObject.gameObject.GetComponent<Transform>();
            miniCube.position   = SonarUtils.CalculateSonarObjectPosition(realObjectTransform.position, SubmarineWaveSource.position, SonarScale, transform.position);
            var aabb = hitObject.bounds;
            miniCube.localScale = aabb.size * SonarScale * ObjectSizeMultiplier;
            miniCube.GetComponent<MeshFilter>().mesh = hitObject.gameObject.TryGetComponent<MeshFilter>(out var meshFilter) 
                ? meshFilter.mesh 
                : FallbackMesh;
        }

        void OnDrawGizmosSelected() {
            Gizmos.DrawWireSphere(SubmarineWaveSource.position, SonarRadius);
            Gizmos.DrawWireSphere(transform.position, VisualSphereRadius);
        }
    }
}
