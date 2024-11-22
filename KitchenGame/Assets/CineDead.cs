// using UnityEngine;
// using Cinemachine;
//
// [ExecuteInEditMode]
// [SaveDuringPlay]
// [AddComponentMenu("Cinemachine/Extensions/SmoothDeadZoneTransposer")]
// public class SmoothDeadZoneTransposer : CinemachineExtension
// {
//     public Vector2 DeadZoneSize = new Vector2(0.5f, 0.5f); // Adjust for dead zone area
//     public float SmoothingSpeed = 5f; // Speed for smooth movement
//
//     private Vector3 lastTargetPosition;
//
//     protected override void Awake()
//     {
//         base.Awake();
//         if (VirtualCamera.Follow != null)
//             lastTargetPosition = VirtualCamera.Follow.position;
//     }
//
//     protected override void PostPipelineStageCallback(
//         CinemachineVirtualCameraBase vcam,
//         CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
//     {
//         if (VirtualCamera.Follow == null || stage != CinemachineCore.Stage.Body)
//             return;
//
//         // Calculate target delta from last known position
//         Vector3 targetPosition = VirtualCamera.Follow.position;
//         Vector3 delta = targetPosition - lastTargetPosition;
//
//         // Convert delta to camera space to apply dead zone
//         Vector3 deltaCameraSpace = state.RawOrientation * delta;
//         float deltaX = Mathf.Abs(deltaCameraSpace.x);
//         float deltaY = Mathf.Abs(deltaCameraSpace.y);
//
//         // Check if target position has moved outside dead zone
//         if (deltaX > DeadZoneSize.x || deltaY > DeadZoneSize.y)
//         {
//             // Smoothly move the camera towards the target
//             Vector3 smoothedPosition = Vector3.Lerp(
//                 state.FinalPosition,
//                 targetPosition - delta,
//                 SmoothingSpeed * deltaTime
//             );
//             state.PositionCorrection += smoothedPosition - state.FinalPosition;
//             lastTargetPosition = targetPosition;
//         }
//     }
// }