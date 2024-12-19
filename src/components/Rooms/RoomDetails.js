import React, { useEffect, useState } from 'react';
import DeviceCard from '../Devices/DeviceCard';
import {getDevices, createDevice, getDevicesByRoom} from "../../services/deviceService";
import './RoomDetails.css'; // Импортируем стили

const RoomDetails = ({ room, onEditDevice, onDeleteDevice }) => {
    const [devices, setDevices] = useState([]);
    const [newDeviceName, setNewDeviceName] = useState('');
    const [schedules, setSchedules] = useState([]); // New state for schedules
    const [scheduleStartTime, setScheduleStartTime] = useState('');
    const [scheduleEndTime, setScheduleEndTime] = useState('');
    const [status, setStatus] = useState('0'); // Default status to '0'
    const [typeField, setTypeField] = useState(''); // New state for typeField

    useEffect(() => {
        const fetchDevices = async () => {
            const data = await getDevicesByRoom(room.id); // Ensure room.id is used
            setDevices(data);
        };
        fetchDevices();
    }, [room.id]);

    const handleAddSchedule = () => {
        if (!scheduleStartTime || !scheduleEndTime) return; // Validate input

        const newSchedule = {
            startTime: scheduleStartTime,
            endTime: scheduleEndTime,
        };

        setSchedules((prevSchedules) => [...prevSchedules, newSchedule]); // Add new schedule to the array
        setScheduleStartTime(''); // Clear the input field
        setScheduleEndTime(''); // Clear the input field
    };

    const handleAddDevice = async () => {
        if (!newDeviceName) return; // Validate input

        const newDevice = {
            name: newDeviceName,
            roomId: room.id, // Use roomId instead of room
            schedules: schedules, // Include schedulesId
            status: Number(status), // Include status
            typeField: typeField, // Include typeField
        };
        console.log(newDevice);

        try {
            const createdDevice = await createDevice(newDevice);
            setDevices([...devices, createdDevice]); // Update the devices list
            setNewDeviceName(''); // Clear the input field
            setSchedules([]); // Clear schedulesId
            setStatus('0'); // Reset status to default
            setTypeField(''); // Clear typeField
        } catch (error) {
            console.error("Error adding device:", error);
        }
    };

    return (
        <div className="room-details-container">
            <h2 className="room-details-title">{room.name}</h2>
            <div className="device-card-container">
                {devices.length > 0 ? (
                    devices.map((device) => (
                        <DeviceCard
                            key={device.id}
                            device={device}
                            onEdit={onEditDevice}
                            onDelete={onDeleteDevice}
                        />
                    ))
                ) : (
                    <div className="no-devices-template">
                        <p>No devices found in this room.</p>
                    </div>
                )}
            </div>
                <div className="add-device-form">
                    <input
                        type="text"
                        placeholder="Device Name"
                        value={newDeviceName}
                        onChange={(e) => setNewDeviceName(e.target.value)}
                    />
                    <select
                        value={status}
                        onChange={(e) => setStatus(e.target.value)} // Update status based on selection
                    >
                        <option value="0">Inactive</option>
                        <option value="1">Active</option>
                    </select>
                    <input
                        type="text"
                        placeholder="Type Field"
                        value={typeField}
                        onChange={(e) => setTypeField(e.target.value)}
                    />
                    <div>
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
                    <button className="add-device-button" onClick={handleAddDevice}>
                        Add Device
                    </button>
                <div className="schedules-list">
                    <h3>Schedules</h3>
                    {schedules.length > 0 ? (
                        schedules.map((schedule, index) => (
                            <p key={index} className="schedule-item">
                                {schedule.startTime} - {schedule.endTime}
                            </p>
                        ))
                    ) : (
                        <p>No schedules added.</p>
                    )}
                </div>
            </div>
        </div>
    );
};

export default RoomDetails;