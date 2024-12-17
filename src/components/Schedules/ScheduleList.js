import React, { useEffect, useState } from 'react';
import { getSchedules, deleteSchedule } from '../../services/scheduleService';

const ScheduleList = ({ onEdit }) => {
    const [schedules, setSchedules] = useState([]);

    useEffect(() => {
        const fetchSchedules = async () => {
            const data = await getSchedules({});
            setSchedules(data);
        };
        fetchSchedules();
    }, []);

    const handleDelete = async (scheduleId) => {
        await deleteSchedule(scheduleId);
        setSchedules(schedules.filter(schedule => schedule.id !== scheduleId));
    };

    return (
        <div>
            <h2>Schedules</h2>
            <ul>
                {schedules.map(schedule => (
                    <li key={schedule.id}>
                        {schedule.startTime} - {schedule.endTime}
                        <button onClick={() => onEdit(schedule)}>Edit</button>
                        <button onClick={() => handleDelete(schedule.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default ScheduleList;