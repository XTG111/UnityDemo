using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FOV : MonoBehaviour
{
    public float viewRadius; // 视距
    [Range(0, 360)]
    public float viewAngle;  // 视角

    public LayerMask targetMask;  // 目标物体层
    public LayerMask obstacleMask;  // 障碍物层

    public int meshResolution = 10;  // 视锥体分段数
    public LineRenderer lineRenderer;  // 线渲染器，用于绘制视锥体

    public Collider2D enemyTarget = null;
    public Collider2D obstacleTaget = null;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        //DrawFieldOfView(); // 可以禁用
        FindEnemyTargets();
        FindObstacleTargets();
    }

    void DrawFieldOfView()
    {
        var stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        var stepAngleSize = viewAngle / stepCount;

        var viewPoints = new Vector3[stepCount + 1];
        for (var i = 0; i <= stepCount; i++)
        {
            var angle = transform.eulerAngles.z - viewAngle / 2 + stepAngleSize * i;
            viewPoints[i] = transform.position + DirFromAngle(angle, false) * viewRadius;
        }

        lineRenderer.positionCount = viewPoints.Length;
        lineRenderer.SetPositions(viewPoints);
    }

    //找到敌人
    void FindEnemyTargets()
    {
        //将前方圆形内的所有敌人存储在一起
        var targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        foreach (var target in targetsInViewRadius)
        {
            var position = transform.position;
            var position1 = target.transform.position;
            var directionToTarget = (position1 - position).normalized;
            var distanceToTarget = Vector3.Distance(position, position1);

            //判断当前敌人是否超过视野或者之间有其他障碍物
            if (!(Vector3.Angle(transform.right, directionToTarget) < viewAngle / 2)) continue;
            if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
            {
                // 目标在视野内且没有被障碍物遮挡
                if(!enemyTarget) enemyTarget = target;
            }
        }
    }
    
    //找到前方的障碍物
    void FindObstacleTargets()
    {
        //将前方圆形内的所有敌人存储在一起
        var targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, obstacleMask);

        foreach (var target in targetsInViewRadius)
        {
            var position = transform.position;
            var position1 = target.transform.position;
            var directionToTarget = (position1 - position).normalized;
            var distanceToTarget = Vector3.Distance(position, position1);

            //判断当前敌人是否超过视野或者之间有其他障碍物
            if (!(Vector3.Angle(transform.right, directionToTarget) < viewAngle / 2)) continue;
            if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, targetMask))
            {
                // 障碍物在视野内且没有被敌人遮挡
                if(!obstacleTaget) obstacleTaget = target;
            }
        }
    }

    private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }
}
