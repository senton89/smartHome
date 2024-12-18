import React, { useEffect, useState } from 'react';
import './DeviceCard.css'; // Импортируем стили
import { createSchedule} from '../../services/scheduleService'; // Import the schedule service
import { findSchedules } from "../../services/deviceService";

const DeviceCard = ({ device, onEdit, onDelete }) => {
    const [scheduleStartTime, setScheduleStartTime] = useState('');
    const [scheduleEndTime, setScheduleEndTime] = useState('');
    const [schedules, setSchedules] = useState([]); // State to hold schedules

    useEffect(() => {
        const fetchSchedules = async () => {
            try {
                const fetchedSchedules = await findSchedules(device.id);
                setSchedules(fetchedSchedules); // Set the fetched schedules
            } catch (error) {
                console.error("Error fetching schedules:", error);
            }
        };

        fetchSchedules();
    }, [device.id]); // Fetch schedules when the device ID changes

    const handleAddSchedule = async () => {
        if (!scheduleStartTime || !scheduleEndTime) return; // Validate input

        const newSchedule = {
            device: device.id,
            startTime: scheduleStartTime,
            endTime: scheduleEndTime,
        };

        try {
            const createdSchedule = await createSchedule(newSchedule);
            setSchedules((prevSchedules) => [...prevSchedules, createdSchedule]); // Update the schedules list
            setScheduleStartTime(''); // Clear the input field
            setScheduleEndTime(''); // Clear the input field
        } catch (error) {
            console.error("Error adding schedule:", error);
        }
    };

    return (
        <div className="device-card">
            <h3 className="device-card-title">{device.name}</h3>
            <p className="device-card-status">Status: {device.status}</p>
            <div className="device-card-content">
                <div className="flex justify-between mt-2">
                    <button
                        className="device-card-button"
                        onClick={() => onEdit(device)}
                    >
                        Edit
                    </button>
                    <button
                        className="device-card-button device-card-button-delete"
                        onClick={() => onDelete(device.id)}
                    >
                        Delete
                    </button>
                </div>
                <div className="schedule-container">
                    <h2 className="device-card-title">Schedule</h2>
                    {schedules.length > 0 ? (
                        schedules.map((schedule) => (
                            <p key={schedule.id} className="device-card-schedule">
                                {schedule.startTime} - {schedule.endTime}
                            </p>
                        ))
                    ) : (
                        <p>No schedules available.</p>
                    )}
                    <input
                        type="time"
                        value={scheduleStartTime}
                        onChange={(e) => setScheduleStartTime(e.target.value)}
                        placeholder="Start Time"
                    />
                    <input
                        type="time"
                        value={scheduleEndTime}
                        onChange={(e) => setScheduleEndTime(e.target.value)}
                        placeholder="End Time"
                    />
                    <button onClick={handleAddSchedule}>Add Schedule</button>
                </div>
            </div>
        </div>
    );
};

export default DeviceCard;