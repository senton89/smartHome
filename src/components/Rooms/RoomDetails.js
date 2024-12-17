import React, {useEffect, useState} from 'react';
import DeviceCard from '../Devices/DeviceCard';
import ScheduleList from '../Schedules/ScheduleList';
import {getDevices} from "../../services/deviceService";

const RoomDetails = ({ room, onEditDevice, onDeleteDevice }) => {
    const [devices, setDevices] = useState([])

    useEffect(() => {
        const fetchDevices = async () => {
            const data = await getDevices({});
            setDevices(data);
        };
        fetchDevices();
    }, []);

    console.log(devices);
    return (
        <div className="bg-white p-4 rounded-lg shadow-md">
            <h2 className="text-2xl font-semibold text-purple-800">{room.name}</h2>
            <div className="mt-4 space-y-4">
                {devices.map((device) => (
                    <DeviceCard
                        key={device.id}
                        device={device}
                        onEdit={onEditDevice}
                        onDelete={onDeleteDevice}
                    />
                ))}
                <button className="mt-4 bg-purple-600 text-white p-2 rounded" onClick={() => {/* функция добавления устройства */}}>
                    Add Device
                </button>
            </div>
            <ScheduleList roomId={room.id} />
        </div>
    );
};

export default RoomDetails;