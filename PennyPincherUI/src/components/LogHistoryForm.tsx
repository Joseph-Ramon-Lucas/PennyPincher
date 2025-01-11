import { useState } from 'react';


function LogHistoryForm() {
    const [item, setItem] = useState();

    return (
        <>
            <div>
                <h2>Log History</h2>
                <p>Log all purchases to view your financial history</p>
                <form>
                    <textarea/>
                </form>
            </div>
        </>
    )
}