import React, { useEffect, useState } from 'react';
import DeviceCard from '../Devices/DeviceCard';
import ScheduleList from '../Schedules/ScheduleList';
import { getDevices } from "../../services/deviceService";
import './RoomDetails.css'; // Импортируем стили

const RoomDetails = ({ room, onEditDevice, onDeleteDevice }) => {
    const [devices, setDevices] = useState([]);

    const testDevices = [
        {
            id: "1",
            name: "device1",
            room: "1"
        },
        {
            id: "2",
            name: "device2",
            room: "1"
        },
        {
            id: "3",
            name: "device3",
            room: "1"
        }
    ];

    useEffect(() => {
        const fetchDevices = async () => {
            // const data = await getDevices({});
            // setDevices(data);
            setDevices(testDevices);
        };
        fetchDevices();
    }, []);

    console.log(devices);
    return (
        <div className="room-details-container">
            <h2 className="room-details-title">{room.name}</h2>
            <div className="device-card-container">
                {devices.map((device) => (
                    <DeviceCard
                        key={device.id}
                        device={device}
                        onEdit={onEditDevice}
                        onDelete={onDeleteDevice}
                    />
                ))}
                <button className="add-device-button" onClick={() => {/* функция добавления устройства */}}>
                    Add Device
                </button>
            </div>
        </div>
    );
};

export default RoomDetails;