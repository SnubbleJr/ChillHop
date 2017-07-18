using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRecord
{
    public int position { get; set; }
    public float time { get; set; }

    public TimeRecord(int pos, float t)
    {
        position = pos;
        time = t;
    }
}

public class TimeRecordController : MonoBehaviour
{
    public GameObject timeRecordPrefab;

    [Range(0, 10)]
    public float recordsToShow = 5;

    private List<TimeRecord> records = new List<TimeRecord>();
    
    private List<TimeRecordView> recordViews = new List<TimeRecordView>();

    private float currentRecordStartTime;

    void OnEnable()
    {
        StartPointBehaviour.runStarted += startNewRecord;
        EndPointBehaviour.runEnded += endNewRecord;
    }

    void OnDisable()
    {
        StartPointBehaviour.runStarted -= startNewRecord;
        EndPointBehaviour.runEnded -= endNewRecord;
    }

    void startNewRecord(float startTime)
    {
        currentRecordStartTime = startTime;
    }

    void endNewRecord(float endTime)
    {
        float recordTime = endTime - currentRecordStartTime;
        int position = getRecordPosition(recordTime);

        TimeRecord currentRecord = new TimeRecord(position, recordTime);

        insertNewRecord(currentRecord);
        updateRecordViews();
    }

    int getRecordPosition(float time)
    {
        int highestPos = records.Count + 1;

        for (int i = 0; i < records.Count; i++)
            if (time < records[i].time)
                if (highestPos > records[i].position)
                    highestPos = records[i].position;

        return highestPos;
    }

    void insertNewRecord(TimeRecord record)
    {
        for (int i = 0; i < records.Count; i++)
            if (record.position <= records[i].position)
                ++records[i].position;

        records.Add(record);
    }

    void updateRecordViews()
    {
        TimeRecord newestRecord = records[records.Count - 1];

        if (records.Count > recordViews.Count && recordViews.Count < recordsToShow)
        {
            TimeRecordView recordView = Instantiate(timeRecordPrefab, transform).GetComponent<TimeRecordView>();
            recordViews.Add(recordView);
        }

        for (int i = 0; i < recordViews.Count; i++)
        {
            TimeRecord record = getRecord(i+1);
            if (record != null)
                recordViews[i].setRecord(record);

            if (record.position == newestRecord.position)
                recordViews[i].setAsNewRecord();
        }
    }

    TimeRecord getRecord(int position)
    {
        for (int i = 0; i < records.Count; i++)
            if (position == records[i].position)
                return records[i];

        return null;
    }
}
