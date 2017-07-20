using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRecord
{
    public int Position { get; set; }
    public float Time { get; set; }

    public TimeRecord(int pos, float t)
    {
        Position = pos;
        Time = t;
    }
}

public class TimeRecordController : MonoBehaviour
{
    public GameObject TimeRecordPrefab;

    [Range(0, 10)]
    public float RecordsToShow = 5;

    private readonly List<TimeRecord> records = new List<TimeRecord>();
    
    private readonly List<TimeRecordView> recordViews = new List<TimeRecordView>();

    private float currentRecordStartTime;

    private void OnEnable()
    {
        StartPointBehaviour.runStarted += StartNewRecord;
        EndPointBehaviour.runEnded += EndNewRecord;
    }

    private void OnDisable()
    {
        StartPointBehaviour.runStarted -= StartNewRecord;
        EndPointBehaviour.runEnded -= EndNewRecord;
    }

    private void StartNewRecord(float startTime)
    {
        currentRecordStartTime = startTime;
    }

    private void EndNewRecord(float endTime)
    {
        float recordTime = endTime - currentRecordStartTime;
        int position = GetRecordPosition(recordTime);

        TimeRecord currentRecord = new TimeRecord(position, recordTime);

        InsertNewRecord(currentRecord);
        UpdateRecordViews();
    }

    private int GetRecordPosition(float time)
    {
        int highestPos = records.Count + 1;

        for (int i = 0; i < records.Count; i++)
            if (time < records[i].Time)
                if (highestPos > records[i].Position)
                    highestPos = records[i].Position;

        return highestPos;
    }

    private void InsertNewRecord(TimeRecord record)
    {
        for (int i = 0; i < records.Count; i++)
            if (record.Position <= records[i].Position)
                ++records[i].Position;

        records.Add(record);
    }

    private void UpdateRecordViews()
    {
        TimeRecord newestRecord = records[records.Count - 1];

        if (records.Count > recordViews.Count && recordViews.Count < RecordsToShow)
        {
            TimeRecordView recordView = Instantiate(TimeRecordPrefab, transform).GetComponent<TimeRecordView>();
            recordViews.Add(recordView);
        }

        for (int i = 0; i < recordViews.Count; i++)
        {
            TimeRecord record = GetRecord(i+1);
            if (record != null)
            {
                recordViews[i].SetRecord(record);

                if (record.Position == newestRecord.Position)
                    recordViews[i].SetAsNewRecord();
            }
        }
    }

    private TimeRecord GetRecord(int position)
    {
        for (int i = 0; i < records.Count; i++)
            if (position == records[i].Position)
                return records[i];

        return null;
    }
}
