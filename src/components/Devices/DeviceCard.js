import React, { useEffect, useState } from 'react';
import './DeviceCard.css'; // Импортируем стили
import { createSchedule } from '../../services/scheduleService'; // Import the schedule service
import { findSchedules, updateDevice } from "../../services/deviceService"; // Import the updateDevice function

const DeviceCard = ({ device, onDelete }) => {
    const [scheduleStartTime, setScheduleStartTime] = useState('');
    const [scheduleEndTime, setScheduleEndTime] = useState('');
    const [schedules, setSchedules] = useState([]); // State to hold schedules
    const [isEditing, setIsEditing] = useState(false); // State to manage editing mode
    const [deviceName, setDeviceName] = useState(device.name); // State for device name
    const [deviceStatus, setDeviceStatus] = useState(device.status); // State for device status

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

    const handleUpdateDevice = async () => {
        try {
            const updatedDevice = {
                name: deviceName,
                status: deviceStatus === "Active" ? 1 : 0, // Convert status to 1 or 0
                schedules: schedules.map(schedule => schedule.id), // Send schedule IDs
                roomId: device.room,
                type: device.type
            };

            console.log(updatedDevice);
            await updateDevice(device.id, updatedDevice);
            setIsEditing(false); // Exit editing mode
        } catch (error) {
            console.error("Error updating device:", error);
        }
    };

    return (
        <div className="device-card">
            {isEditing ? (
                <div>
                    <input
                        type="text"
                        value={deviceName}
                        onChange={(e) => setDeviceName(e.target.value)}
                    />
                    <select
                        value={deviceStatus}
                        onChange={(e) => setDeviceStatus(e.target.value)}
                    >
                        <option value="Active">Active</option>
                        <option value="Inactive">Inactive</option>
                    </select>
                    <button onClick={handleUpdateDevice}>Save</button>
                    <button onClick={() => setIsEditing(false)}>Cancel</button>
                </div>
            ) : (
                <div>
                    <h3 className="device-card-title">{device.name}</h3>
                    <p className="device-card-status">Status: {device.status===1?"Active":"Inactive"}</p>
                    <div className="flex justify-between mt-2">
                        <button
                            className="device-card-button"
                            onClick={() => setIsEditing(true)} // Set editing mode
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
                </div>
            )}
            <div className="device-card-content">
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