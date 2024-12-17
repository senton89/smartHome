import React, { useEffect, useState } from 'react';
import { getRooms, deleteRoom } from '../../services/roomService';

const RoomList = ({ onEdit }) => {
    const [rooms, setRooms] = useState([]);

    useEffect(() => {
        const fetchRooms = async () => {
            const data = await getRooms({});
            setRooms(data);
        };
        fetchRooms();
    }, []);

    const handleDelete = async (roomId) => {
        await deleteRoom(roomId);
        setRooms(rooms.filter(room => room.id !== roomId));
    };

    return (
        <div>
            <h2>Rooms</h2>
            <ul>
                {rooms.map(room => (
                    <li key={room.id}>
                        {room.name}
                        <button onClick={() => onEdit(room)}>Edit</button>
                        <button onClick={() => handleDelete(room.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default RoomList;